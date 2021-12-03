using Collabile.DataAccess.Models;
using Collabile.DataAccess.Repositories;
using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projRepo;
        private readonly IReleaseRepository _releaseRepo;

        public ProjectService(IProjectRepository projRepo, IReleaseRepository releaseRepo)
        {
            _projRepo = projRepo;
            _releaseRepo = releaseRepo;
        }

        public string AddProject(Project project)
        {
            return _projRepo.Add(project);
        }

        public void DeleteProject(string projectKey)
        {
            _projRepo.Delete(projectKey);
        }

        public List<ProjectSummary> GetAllProjects()
        {
            return _projRepo.FetchAll(null);
        }

        public Project GetProjectDetails(string projectKey)
        {
            Project proj = _projRepo.FetchById(projectKey);
            proj.Releases = _releaseRepo.FetchAll($@"Project = '@{projectKey}'");
            return proj;
        }

        public void UpdateProject(Project project)
        {
            _projRepo.Update(project);
        }
    }
}
