using CrowdFundT2.Core.Data;
using CrowdFundT2.Core.Model;
using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CrowdFundT2.Core.Services
{
    public class PackageService : IPackageService
    {
        private CrowdFundT2DbContext context_;
        public PackageService(CrowdFundT2DbContext context)
        {
            context_ = context;
        }

        public ApiResult<Package> CreatePackage(CreatePackageOptions options)
        {
            if (options == null)
            {
                return ApiResult<Package>.Failed(StatusCode.BadRequest, "Null options");
            }

            if (options.ProjectId == null)
            {
                return ApiResult<Package>.Failed(StatusCode.BadRequest, "The Project Id is empty");
            }

            if (options.Reward == null || options.Reward.Value <= 0)
            {
                return ApiResult<Package>.Failed(StatusCode.BadRequest, "Package reward is not valid");
            }

            var project = context_
                .Set<Project>()
                .Where(p => p.ProjectId == options.ProjectId)
                .Include(x=>x.Packages)
                .SingleOrDefault();

            foreach (var item in project.Packages)
            {
                if (item.Description == options.Description && item.Reward == options.Reward)
                {
                    return ApiResult<Package>.Failed(StatusCode.BadRequest, "Package with the same options already exists");
                }
            }

            var package = new Package()
            {
                Description = options.Description,
                Reward = options.Reward.Value,
                IsActive = true
            };

            project.Packages.Add(package);
            context_.Update(project);
            context_.Add(package);

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<Package>.Failed(
                        StatusCode.InternalServerError, "Package could not be created");
                }
            }
            catch (Exception ex)
            {
                return ApiResult<Package>.Failed(StatusCode.InternalServerError, ex.ToString());
            }

            return ApiResult<Package>.Successful(package);
        }

        public ApiResult<bool> DeletePackage(int? id)
        {
            if (id == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "The Package Id is empty");
            }

            var project = context_
                .Set<Project>()
                .Where(p => p.ProjectId == id)
                .SingleOrDefault();

            var package = GetPackageById(id).Data;
            // removing package from package table & FundingPackages List(project class)
            project.Packages.Remove(package);
            context_.Remove(package);
            context_.Update(project);

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<bool>.Failed(
                        StatusCode.InternalServerError, "Package could not be deleted");
                }
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Failed(StatusCode.InternalServerError, ex.ToString());
            }

            return ApiResult<bool>.Successful(true);
        }

        public ApiResult<Package> GetPackageById(int? id)
        {
            if (id == null)
            {
                return ApiResult<Package>.Failed(StatusCode.BadRequest, "Null Package Id");
            }

            var options = new SearchPackageOptions()
            {
                PackageId = id
            };

            return ApiResult<Package>.Successful(SearchPackage(options).Data.SingleOrDefault());
        }

        public ApiResult<IQueryable<Package>> SearchPackage(SearchPackageOptions options)
        {
            if (options == null)
            {
                return ApiResult<IQueryable<Package>>.Failed(StatusCode.BadRequest, "Null options");
            }

            var query = context_
                .Set<Package>()
                .AsQueryable();

            if (options.PackageId != null)
            {
                query = query.Where(p => p.PackageId == options.PackageId);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(p => p.Description == options.Description);
            }

            if (options.Reward != null)
            {
                query = query.Where(p => p.Reward == options.Reward);
            }

            if (options.IsActive != null)
            {
                query = query.Where(p => p.IsActive == options.IsActive);
            }

            if (options.CreatedFrom != null)
            {
                query = query.Where(c => c.Created >= (options.CreatedFrom));
            }

            if (options.CreatedTo != null)
            {
                query = query.Where(c => c.Created <= options.CreatedTo);
            }

            return ApiResult<IQueryable<Package>>.Successful(query.Take(500));
        }

        public ApiResult<bool> UpdatePackage(int? id, UpdatePackageOptions options)
        {
            if (options == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "Null options");
            }

            if (id == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "The Package Id is empty");
            }

            var updatePackage = GetPackageById(id).Data;

            if (options.Description != null)
            {
                updatePackage.Description = options.Description;
            }

            if (options.Reward != null)
            {
                updatePackage.Reward = options.Reward.Value;
            }

            if (options.IsActive != null)
            {
                updatePackage.IsActive = options.IsActive.Value;
            }

            var project = context_
                .Set<Project>()
                .Where(p => p.ProjectId == options.ProjectId)
                .Include(x=>x.Packages)
                .SingleOrDefault();

            foreach (var item in project.Packages)
            {
                if (item.Description == options.Description && item.Reward == options.Reward)
                {
                    return ApiResult<bool>.Failed(StatusCode.BadRequest, "Package with the same options already exists");
                }
            }

            project.Packages.Add(updatePackage);
            context_.Update(project);

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<bool>.Failed(
                        StatusCode.InternalServerError, "Package could not be updated");
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