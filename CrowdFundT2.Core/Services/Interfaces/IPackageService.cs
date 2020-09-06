using CrowdFundT2.Core.Model;
using CrowdFundT2.Core.Services.Options;
using System.Linq;

namespace CrowdFundT2.Core.Services.Interfaces
{
    public interface IPackageService
    {
        ApiResult<Package> CreatePackage(
            CreatePackageOptions options);
        ApiResult<IQueryable<Package>> SearchPackage(
            SearchPackageOptions options);
        ApiResult<bool> UpdatePackage(
            int? id, UpdatePackageOptions options);
        ApiResult<Package> GetPackageById(
            int? id);
        ApiResult<bool> DeletePackage(
            int? id);
    }
}
