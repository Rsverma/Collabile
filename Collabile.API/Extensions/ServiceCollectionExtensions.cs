using Collabile.Api.DataAccess;
using Collabile.Api.Helpers;
using Collabile.Api.Models;
using Collabile.Api.Permission;
using Collabile.Api.Services;
using Collabile.Shared.Constants;
using Collabile.Shared.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;

namespace Collabile.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {

        private static void SetCultureFromServerPreferenceAsync()
        {
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        internal static AppConfiguration GetApplicationSettings(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
            services.Configure<AppConfiguration>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppConfiguration>();
        }

        internal static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //TODO - Lowercase Swagger Documents
                //c.DocumentFilter<LowercaseDocumentFilter>();
                //Refer - https://gist.github.com/rafalkasa/01d5e3b265e5aa075678e0adfd54e23f

                // include all project's xml comments
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.IsDynamic)
                    {
                        var xmlFile = $"{assembly.GetName().Name}.xml";
                        var xmlPath = Path.Combine(baseDirectory, xmlFile);
                        if (File.Exists(xmlPath))
                        {
                            c.IncludeXmlComments(xmlPath);
                        }
                    }
                }

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Collabile.Web",
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                SetCultureFromServerPreferenceAsync();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        internal static IServiceCollection AddCurrentUserService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }

        internal static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>()
                .AddIdentity<CollabileUser, CollabileRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddDefaultTokenProviders();

            return services;
        }

        internal static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IDateTimeService, SystemDateTimeService>();
            //services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));
            //services.AddTransient<IMailService, SMTPMailService>();
            return services;
        }

        internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IRoleClaimService, RoleClaimService>();
            services.AddTransient<ITokenService, IdentityService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IUploadService, UploadService>();
            services.AddTransient<IAuditService, AuditService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddSingleton<ITaskService, TaskService>()
                    .AddSingleton<ISqlDataAccess, SqlDataAccess>();
            return services;
        }

        internal static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, AppConfiguration config)
        {
            var key = Encoding.ASCII.GetBytes(config.Secret);
            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RoleClaimType = ClaimTypes.Role,
                        ClockSkew = TimeSpan.Zero
                    };

                    bearer.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = c =>
                        {
                            if (c.Exception is SecurityTokenExpiredException)
                            {
                                c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                c.Response.ContentType = "application/json";
                                var result = JsonConvert.SerializeObject(Result.Fail("The Token is expired."));
                                return c.Response.WriteAsync(result);
                            }
                            else
                            {
                                c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                c.Response.ContentType = "application/json";
                                var result = JsonConvert.SerializeObject(Result.Fail("An unhandled error has occurred."));
                                return c.Response.WriteAsync(result);
                            }
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";
                                var result = JsonConvert.SerializeObject(Result.Fail("You are not Authorized."));
                                return context.Response.WriteAsync(result);
                            }

                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("You are not authorized to access this resource."));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
            services.AddAuthorization(options =>
            {
                // Here I stored necessary permissions/roles in a constant
                foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
                {
                    var propertyValue = prop.GetValue(null);
                    if (propertyValue is not null)
                    {
                        options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()));
                    }
                }
            });
            return services;
        }
        internal static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
                //.AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                //.AddTransient<IProductRepository, ProductRepository>()
                //.AddTransient<IBrandRepository, BrandRepository>()
                //.AddTransient<IDocumentRepository, DocumentRepository>()
                //.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>()
                //.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        //internal static IServiceCollection AddExtendedAttributesUnitOfWork(this IServiceCollection services)
        //{
        //    return services
        //        .AddTransient(typeof(IExtendedAttributeUnitOfWork<,,>), typeof(ExtendedAttributeUnitOfWork<,,>));
        //}

        //internal static IServiceCollection AddServerStorage(this IServiceCollection services)
        //    => AddServerStorage(services, null);

        //internal static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        //{
        //    return services
        //        .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
        //        .AddScoped<IStorageProvider, ServerStorageProvider>()
        //        .AddScoped<IServerStorageService, ServerStorageService>()
        //        .AddScoped<ISyncServerStorageService, ServerStorageService>()
        //        .Configure<SystemTextJsonOptions>(configureOptions =>
        //        {
        //            configure?.Invoke(configureOptions);
        //            if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
        //                configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
        //        });
        //}


        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        internal static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppConfiguration>());
            return builder;
        }

        //internal static void AddExtendedAttributesValidators(this IServiceCollection services)
        //{
        //    #region AddEditExtendedAttributeCommandValidator

        //    var addEditExtendedAttributeCommandValidatorType = typeof(AddEditExtendedAttributeCommandValidator<,,,>);
        //    var validatorTypes = addEditExtendedAttributeCommandValidatorType
        //        .Assembly
        //        .GetExportedTypes()
        //        .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
        //        .Select(t => new
        //        {
        //            BaseGenericType = t.BaseType,
        //            CurrentType = t
        //        })
        //        .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(AddEditExtendedAttributeCommandValidator<,,,>))
        //        .ToList();

        //    foreach (var validatorType in validatorTypes)
        //    {
        //        var addEditExtendedAttributeCommandType = typeof(AddEditExtendedAttributeCommand<,,,>).MakeGenericType(validatorType.BaseGenericType.GetGenericArguments());
        //        var iValidator = typeof(IValidator<>).MakeGenericType(addEditExtendedAttributeCommandType);
        //        services.AddScoped(iValidator, validatorType.CurrentType);
        //    }

        //    #endregion AddEditExtendedAttributeCommandValidator
        //}

        //public static void AddExtendedAttributesHandlers(this IServiceCollection services)
        //{
        //    var extendedAttributeTypes = typeof(IEntity)
        //        .Assembly
        //        .GetExportedTypes()
        //        .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
        //        .Select(t => new
        //        {
        //            BaseGenericType = t.BaseType,
        //            CurrentType = t
        //        })
        //        .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(AuditableEntityExtendedAttribute<,,>))
        //        .ToList();

        //    foreach (var extendedAttributeType in extendedAttributeTypes)
        //    {
        //        var extendedAttributeTypeGenericArguments = extendedAttributeType.BaseGenericType.GetGenericArguments().ToList();
        //        extendedAttributeTypeGenericArguments.Add(extendedAttributeType.CurrentType);

        //        #region AddEditExtendedAttributeCommandHandler

        //        var tRequest = typeof(AddEditExtendedAttributeCommand<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        var tResponse = typeof(Result<>).MakeGenericType(extendedAttributeTypeGenericArguments.First());
        //        var serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
        //        var implementationType = typeof(AddEditExtendedAttributeCommandHandler<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        services.AddScoped(serviceType, implementationType);

        //        #endregion AddEditExtendedAttributeCommandHandler

        //        #region DeleteExtendedAttributeCommandHandler

        //        tRequest = typeof(DeleteExtendedAttributeCommand<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        tResponse = typeof(Result<>).MakeGenericType(extendedAttributeTypeGenericArguments.First());
        //        serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
        //        implementationType = typeof(DeleteExtendedAttributeCommandHandler<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        services.AddScoped(serviceType, implementationType);

        //        #endregion DeleteExtendedAttributeCommandHandler

        //        #region GetAllExtendedAttributesByEntityIdQueryHandler

        //        tRequest = typeof(GetAllExtendedAttributesByEntityIdQuery<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        tResponse = typeof(Result<>).MakeGenericType(typeof(List<>).MakeGenericType(
        //            typeof(GetAllExtendedAttributesByEntityIdResponse<,>).MakeGenericType(
        //                extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1])));
        //        serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
        //        implementationType = typeof(GetAllExtendedAttributesByEntityIdQueryHandler<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        services.AddScoped(serviceType, implementationType);

        //        #endregion GetAllExtendedAttributesByEntityIdQueryHandler

        //        #region GetExtendedAttributeByIdQueryHandler

        //        tRequest = typeof(GetExtendedAttributeByIdQuery<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        tResponse = typeof(Result<>).MakeGenericType(
        //            typeof(GetExtendedAttributeByIdResponse<,>).MakeGenericType(
        //                extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1]));
        //        serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
        //        implementationType = typeof(GetExtendedAttributeByIdQueryHandler<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        services.AddScoped(serviceType, implementationType);

        //        #endregion GetExtendedAttributeByIdQueryHandler

        //        #region GetAllExtendedAttributesQueryHandler

        //        tRequest = typeof(GetAllExtendedAttributesQuery<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        tResponse = typeof(Result<>).MakeGenericType(typeof(List<>).MakeGenericType(
        //            typeof(GetAllExtendedAttributesResponse<,>).MakeGenericType(
        //                extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1])));
        //        serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
        //        implementationType = typeof(GetAllExtendedAttributesQueryHandler<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        services.AddScoped(serviceType, implementationType);

        //        #endregion GetAllExtendedAttributesQueryHandler

        //        #region ExportExtendedAttributesQueryHandler

        //        tRequest = typeof(ExportExtendedAttributesQuery<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        tResponse = typeof(Result<>).MakeGenericType(typeof(string));
        //        serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
        //        implementationType = typeof(ExportExtendedAttributesQueryHandler<,,,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
        //        services.AddScoped(serviceType, implementationType);

        //        #endregion ExportExtendedAttributesQueryHandler
        //    }
        //}
    }
}
