using CrowdFundT2.Core.Model;
using CrowdFundT2.Core.Services.Options;
using System.Linq;

namespace CrowdFundT2.Core.Services.Interfaces
{
    public interface IProjectService
    {
        ApiResult<Project> CreateProject(
           CreateProjectOptions options);
        ApiResult<IQueryable<Project>> SearchProjects(
            SearchProjectOptions options);
        ApiResult<bool> UpdateProject(
           int? id, UpdateProjectOptions options);
        ApiResult<Project> GetProjectById(int? id);
        ApiResult<bool> DeleteProject(int? id);
    }
}
