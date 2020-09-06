using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CrowdFundT2.Web.Controllers
{
    [Route("Package")]
    public class PackageController : Controller
    {
        private IPackageService packageService_;

        public PackageController(IPackageService packageService)
        {
            packageService_ = packageService;
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] CreatePackageOptions options)
        {
            var pack = packageService_.CreatePackage(options);

            if (!pack.Success)
            {
                return StatusCode((int)pack.ErrorCode, pack.ErrorText);
            }

            return Json(pack);
        }

        [HttpPatch("{id}")]

        public IActionResult Update(int id, [FromBody] UpdatePackageOptions options)
        {
            var result = packageService_.UpdatePackage(id, options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode, result.ErrorText);
            }

            return Json(result);
        }

        [HttpGet]
        public IActionResult Search(SearchPackageOptions options)
        {
            var result = packageService_
                .SearchPackage(options)
                .Data
                .ToList();
            
            return Json(result);
        }

        public IActionResult GetById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var client = packageService_
                .SearchPackage(new SearchPackageOptions()
                {
                    PackageId = id,

                }).Data.SingleOrDefault();

            if (client == null)
            {
                return NotFound();
            }
            return Json(client);
        }
    }
}