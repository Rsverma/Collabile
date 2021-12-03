using Collabile.DataAccess.Models;
using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;
        public ProjectRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> Add(Project project)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("dbo.spProject_Insert", new
                {
                    project.Key,
                    project.Name,
                    project.Description,
                    project.SprintDays,
                    project.StartDate,
                    project.IsPublic,
                    project.Owner
                }, commandType: CommandType.StoredProcedure);
            }

            return project.Key;
        }

        public async Task Delete(string key)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync($@"DELETE FROM [Project]
                WHERE [Key] = @{nameof(key)}", new { key });
            }
        }

        public async Task<List<ProjectSummary>> FetchAll(string filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> FetchById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
