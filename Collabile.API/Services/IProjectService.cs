using Collabile.DataAccess.Models;
using Collabile.Shared.Helper;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public interface IProjectService : IService
    {
        string AddProject(Project project);

        void UpdateProject(Project project);

        void DeleteProject(string projectKey);

        Project GetProjectDetails(string projectKey);

        List<ProjectSummary> GetAllProjects();
    }
}
