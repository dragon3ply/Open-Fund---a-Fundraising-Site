using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFundT2.Web.Controllers
{
    [Route("InvestProject")]
    public class InvestProjectController : Controller
    {
        private IInvestProjectService InvestProjectService_;
        private IProjectService projectService_;

        public InvestProjectController(IInvestProjectService investProjectService, IProjectService projectService)
        {
            InvestProjectService_ = investProjectService;
            projectService_ = projectService;
        }

        [Route("Invest")]
        [HttpPost]
        public IActionResult Invest([FromBody] InvestProjectOptions options)
        {
            var result = InvestProjectService_.InvestProject(options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode, result.ErrorText);
            }

            return Json(result);
        }

        [HttpGet]
        public IActionResult InvestProject(int id)
        {
            var project = projectService_.GetProjectById(id);

            if (!project.Success)
            {
                return StatusCode((int)project.ErrorCode,
                    project.ErrorText);
            }

            return View(project);
        }
    }
}