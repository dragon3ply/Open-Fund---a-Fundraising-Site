using CrowdFundT2.Core.Model;
using CrowdFundT2.Core.Services.Options;
using System.Linq;

namespace CrowdFundT2.Core.Services.Interfaces
{
    public interface IClientService
    {
        ApiResult<Client> CreateClient(
           CreateClientOptions options);

        ApiResult<IQueryable<Client>> SearchClient(
            SearchClientOptions options);

        ApiResult<bool> UpdateClient(
            int? id, UpdateClientOptions options);

        ApiResult<Client> GetClientById(int? id);

        ApiResult<bool> DeleteClient(int? id);
    }
}
