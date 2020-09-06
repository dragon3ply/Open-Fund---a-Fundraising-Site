using CrowdFundT2.Core.Data;
using CrowdFundT2.Core.Model;
using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrowdFundT2.Core.Services
{
    public class ProjectService : IProjectService
    {
        private CrowdFundT2DbContext context_;

        public ProjectService(CrowdFundT2DbContext context)
        {
            context_ = context;
        }
        public ApiResult<Project> CreateProject(CreateProjectOptions options)
        {
            if (options == null)
            {
                return ApiResult<Project>.Failed(StatusCode.BadRequest, "Null options");
            }

            if (options.ClientId == null)
            {
                return ApiResult<Project>.Failed(StatusCode.BadRequest, "Client Id is empty");
            }
            
            var client = context_.Set<Client>()
                .Where(o => o.ClientId == options.ClientId)
                .SingleOrDefault();

            if (client == null)
            {
                return ApiResult<Project>.Failed(StatusCode.BadRequest, "Client Id not valid");
            }

            var project = new Project()
            {
                Title = options.Title,
                Description = options.Description,
                Progress = options.Progress,
                Category = options.Category.Value,
                PostStatusUpdates = options.PostStatusUpdates,
                Photos = options.Photos,
                Videos = options.Videos,
                ProjectCost = options.ProjectCost.Value,
                ClientId = options.ClientId.Value
            };

            client.Projects.Add(project);
            context_.Update(client);

            context_.Add(project);

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<Project>.Failed(
                        StatusCode.InternalServerError, "Project could not be created");
                }
            }
            catch (Exception ex)
            {
                return ApiResult<Project>.Failed(StatusCode.InternalServerError, ex.ToString());
            }

            return ApiResult<Project>.Successful(project);
        }

        public ApiResult<bool> DeleteProject(int? id)
        {
            if (id == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "Null options");
            }

            var proj = GetProjectById(id.Value);
            context_.Remove(proj);

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<bool>.Failed(
                        StatusCode.InternalServerError, "Project could not be deleted");
                }
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Failed(StatusCode.InternalServerError, ex.ToString());
            }

            return ApiResult<bool>.Successful(true);
        }

        public ApiResult<Project> GetProjectById(int? id)
        {
            if (id == null)
            {
                return ApiResult<Project>.Failed(StatusCode.BadRequest, "The Project Id is empty");
            }

            var options = new SearchProjectOptions()
            {
                ProjectId = id
            };

            return ApiResult<Project>.Successful(SearchProjects(options).Data.SingleOrDefault());
        }

        public ApiResult<IQueryable<Project>> SearchProjects(SearchProjectOptions options)
        {
            if (options == null)
            {
                return ApiResult<IQueryable<Project>>.Failed(StatusCode.BadRequest, "Null options");
            }

            var query = context_
                .Set<Project>()
                .AsQueryable();

            var queries = new List<IQueryable<Project>>();

            if (options.ProjectId != null)
            {
                query = query.Where(p => p.ProjectId == options.ProjectId);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                queries.Add(query.Where(p => p.Description.Contains(options.Description)));
            }

            if (options.Category != null)
            {
                queries.Add(query.Where(p => p.Category == options.Category));
            }

            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                queries.Add(query.Where(p => p.Title.Contains(options.Title)));
            }

            if (!string.IsNullOrWhiteSpace(options.Photos))
            {
                queries.Add(query.Where(p => p.Photos == options.Photos));
            }

            if (!string.IsNullOrWhiteSpace(options.Videos))
            {
                queries.Add(query.Where(p => p.Videos == options.Videos));
            }

            if (!string.IsNullOrWhiteSpace(options.PostStatusUpdates))
            {
                queries.Add(query.Where(p => p.PostStatusUpdates == options.PostStatusUpdates));
            }

            if (options.CreatedFrom != null)
            {
                queries.Add(query.Where(c => c.Created >= (options.CreatedFrom)));
            }

            if (options.CreatedTo != null)
            {
                queries.Add(query.Where(c => c.Created <= options.CreatedTo));
            }

            if (options.TotalAmountFrom != null)
            {
                queries.Add(query.Where(c => c.ProjectCost >= options.TotalAmountFrom));
            }

            if (options.TotalAmountTo != null)
            {
                queries.Add(query.Where(c => c.ProjectCost <= options.TotalAmountTo));

            }

            if (queries.Count > 0)
            {
                query = queries.Aggregate(query.Where(y => false), (q1, q2) => q1.Union(q2));
            }

            return ApiResult<IQueryable<Project>>.Successful(query.Take(500));
        }

        public ApiResult<bool> UpdateProject(int? id, UpdateProjectOptions options)
        {
            if (options == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "Null options");
            }

            if (id == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "The Project Id is empty");
            }

            var updateProject = context_
                .Set<Project>()
                .Where(p => p.ProjectId == id)
                .SingleOrDefault();

            if (options.Title != null)
            {
                updateProject.Title = options.Title;
            }

            if (options.Description != null)
            {
                updateProject.Description = options.Description;
            }

            if (options.Photos != null)
            {
                updateProject.Photos = options.Photos;
            }

            if (options.Videos != null)
            {
                updateProject.Videos = options.Videos;
            }

            if (options.PostStatusUpdates != null)
            {
                updateProject.PostStatusUpdates = options.PostStatusUpdates;
            }

            if (options.TotalAmount > 0)
            {
                updateProject.ProjectCost = options.TotalAmount.Value;
            }

            if (options.Progress != 0)
            {
                updateProject.Progress = options.Progress;
            }

            if (options.FundingPackages != null)
            {
                updateProject.Packages = options.FundingPackages;
            }

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<bool>.Failed(
                        StatusCode.InternalServerError, "Project could not be Updated");
                }
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Failed(StatusCode.InternalServerError, ex.ToString());
            }

            return ApiResult<bool>.Successful(true);
        }

    }
}
