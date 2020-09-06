using CrowdFundT2.Core.Services.Options;

namespace CrowdFundT2.Core.Services.Interfaces
{
    public interface IInvestProjectService
    {
        ApiResult<bool> InvestProject(InvestProjectOptions options);
    }
}
