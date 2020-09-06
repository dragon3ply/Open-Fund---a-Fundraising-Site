using CrowdFundT2.Core.Data;
using CrowdFundT2.Core.Model;
using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CrowdFundT2.Core.Services
{
    public class InvestProjectService : IInvestProjectService
    {
        private CrowdFundT2DbContext context_;
        private IPackageService package_;


        public InvestProjectService(CrowdFundT2DbContext context, IPackageService package)
        {
            context_ = context;
            package_ = package;
        }
        public ApiResult<bool> InvestProject(InvestProjectOptions options)
        {
            if(options==null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "Null options");
            }

            if (options.ClientId == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "The Client Id is empty");
            }

            var client = context_
                .Set<Client>()
                .Where(x => x.ClientId == options.ClientId)
                .Include(x=>x.InvestedProjects)
                .SingleOrDefault();

            if (client == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "The Client doesn't exist in Database");
            }

            /*foreach (var item in client.InvestedProjects)
            {
                if (options.ProjectId == item.ProjectId)
                {
                    return ApiResult<bool>.Failed(StatusCode.BadRequest, "You are the creator of the project you cant invest")
                }
            }*/

            if (options.ProjectId == null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "The Project Id is empty");
            }

            var proj= context_
                .Set<Project>().Where(x => x.ProjectId == options.ProjectId).Include(x => x.Packages).SingleOrDefault();

            if(proj==null)
            {
                return ApiResult<bool>.Failed(StatusCode.BadRequest, "The Project doesn't exist in Database");
            }

            Package newPackage = null;

            //Check if a Package exist
            if (options.PackageId != null)
            {
                var projPack = proj.Packages;
                foreach (var item in projPack)
                {
                    if (item.PackageId == options.PackageId)
                    {
                        newPackage = item;
                    }
                }

                if (newPackage == null)
                {
                    return ApiResult<bool>.Failed(StatusCode.BadRequest, 
                        "The package you are trying to invest does not exist in the current project");
                }

            }

            var invProj = context_
                .Set<InvestedProject>()
                .Where(x => x.ProjectId == options.ProjectId)
                .Where(x => x.ClientId == options.ClientId)
                .Include(x => x.InvestedPackages)
                .SingleOrDefault(); 


            //Check if Backer has already invested in this project
            if (invProj != null)
            {

                //Check if a Package exist
                if (options.PackageId != null)
                {
                    //loop all Backer's invested packages
                    var x = invProj.InvestedPackages.Where(x => x.PackageId == options.PackageId).Count();
                    if (x > 0)
                    {
                        return ApiResult<bool>.Failed(StatusCode.Forbidden, "You have already invest this package");
                    }
                    else  //Do invest
                    {
                        InvestedPackage invPackage = new InvestedPackage()
                        {
                            PackageId = options.PackageId.Value,
                            InvestedProjectId = options.ProjectId.Value
                        };

                        invProj.InvestedPackages.Add(invPackage);

                        context_.Update(client);

                        invProj.InvestedAmount += newPackage.Reward;
                        proj.Earnings += invProj.InvestedAmount.Value;

                        /*context_.Update(proj);*/
                    }
                }
                else
                {
                    if (options.InvestedAmount == null ||
                      options.InvestedAmount.Value <= 0)
                    {
                        return ApiResult<bool>.Failed(StatusCode.BadRequest,
                        "Enter a valid invested amount");
                    }
                    invProj.InvestedAmount += options.InvestedAmount.Value;
                    proj.Earnings += invProj.InvestedAmount.Value;

                    context_.Update(client);
                    /*context_.Update(proj);*/
                }

                try
                {
                    var rows = context_.SaveChanges();
                    if (rows <= 0)
                    {
                        return ApiResult<bool>.Failed(
                            StatusCode.InternalServerError,
                            "The Invested Project couldn't be added in Database");
                    }
                }
                catch (Exception ex)
                {
                    return ApiResult<bool>.Failed(StatusCode.InternalServerError, ex.ToString());
                }

                return ApiResult<bool>.Successful(true);

            }
            else
            {
                
                if (options.PackageId != null)
                {
                    invProj = new InvestedProject()
                    {
                        ProjectId = options.ProjectId.Value,
                        ClientId = options.ClientId.Value,
                        InvestedAmount = newPackage.Reward
                    };
                    proj.Earnings += invProj.InvestedAmount.Value;

                    InvestedPackage invPackage = new InvestedPackage()
                    {
                        PackageId = options.PackageId.Value,
                        InvestedProjectId = options.ProjectId.Value
                    };

                    invProj.InvestedPackages.Add(invPackage);

                    context_.Update(client);
                }
                else
                {

                    if (options.InvestedAmount == null ||
                      options.InvestedAmount.Value <= 0)
                    {
                        return ApiResult<bool>.Failed(StatusCode.BadRequest,
                        "Enter a valid invested amount");
                    }
                    invProj = new InvestedProject()
                    {
                        ProjectId = options.ProjectId.Value,
                        ClientId = options.ClientId.Value,
                        InvestedAmount = options.InvestedAmount.Value
                    };
                    proj.Earnings += invProj.InvestedAmount.Value;

                    context_.Update(client);
                }
            }

            context_.Add(invProj);

            client.InvestedProjects.Add(invProj);

            try
            {
                var rows = context_.SaveChanges();
                if (rows <= 0)
                {
                    return ApiResult<bool>.Failed(
                        StatusCode.InternalServerError,
                        "The Invested Package couldn't be added in Database");
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
