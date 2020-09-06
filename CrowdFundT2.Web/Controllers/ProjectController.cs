using CrowdFundT2.Core;
using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using CrowdFundT2.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CrowdFundT2.Web.Controllers
{
    [Route("Project")]
    public class ProjectController : Controller
    {
        private IProjectService projectService_;

        public ProjectController(IProjectService projectService)
        {
            projectService_ = projectService;
        }

        [HttpGet]
        public IActionResult Project([FromQuery] string title, [FromQuery] string description, [FromQuery] ProjectCategory? category)
        {
            var projects = projectService_
                .SearchProjects(
                    new SearchProjectOptions() { 
                        Title = title,
                        Description = description,
                        Category = category
                    })
                .Data
                .Include(x => x.Packages)
                .Include(y => y.Client)
                .ToList();

            var cflist = new CfModel()
            {
                Projects = projects
            };

            return View(cflist);
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] CreateProjectOptions options, [FromQuery] int numberOfPackages)
        {
            var createdp = projectService_.CreateProject(options);

            if(!createdp.Success)
            {
                return StatusCode((int)createdp.ErrorCode, createdp.ErrorText);
            }

            return Json(new
            {
                result = "Redirect",
                url = Url.Action("AddPackages", "Project",
                new
                {
                    projectId = createdp.Data.ProjectId,
                    numberOfPackages,
                })
            });
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult CreateNewProject()
        {
            return View();
        }

        [Route("AddPackages")]
        [HttpGet]
        public IActionResult AddPackages([FromQuery] int projectId,[FromQuery] int numberOfPackages)
        {
            var addPackageModel = new AddPackageModel()
            {
                projectId = projectId,
                numberOfPackages = numberOfPackages
            };

            return View(addPackageModel);
        }

        [HttpPatch("Update/{id}")]
        public IActionResult Update(int id,[FromBody] UpdateProjectOptions options)
        {
            var result = projectService_.UpdateProject(id, options);

            if(!result.Success)
            {
                return StatusCode((int)result.ErrorCode, result.ErrorText);
            }

            return Json(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = projectService_.DeleteProject(id);

            if(!result.Success)
            {
                return StatusCode((int)result.ErrorCode, result.ErrorText);
            }

            return Json(result);
        }

        [Route("SearchProject")]
        [HttpPost]
        public IActionResult SearchProject([FromBody] SearchProjectOptions options)
        {
            var projects = projectService_
                .SearchProjects(options)
                .Data
                .Include(x => x.Packages)
                .ToList();

            var cfModel = new CfModel()
            {
                Projects = projects
            };

            return View(cfModel);
        }

        [Route("SearchByFilters")]
        [HttpPatch]
        public IActionResult SearchByFilters([FromBody] SearchProjectOptions options)
        {
            var projects = projectService_
                .SearchProjects(options)
                .Data
                .Include(x => x.Packages)
                .ToList();

            var cfModel = new CfModel()
            {
                Projects = projects
            };
            
            return View(cfModel);
        }

        public IActionResult GetById(int? id)
        {
            if(id == null)
            {
                return BadRequest();

            }

            var project = projectService_
                .SearchProjects(new SearchProjectOptions()
                {
                    ProjectId = id,
                }).Data.SingleOrDefault();

            if (project == null)
            {
                return NotFound();
            }
            return Json(project);
        }

        [HttpGet("Details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var project = projectService_
            .SearchProjects(new SearchProjectOptions()
            {
                ProjectId = id,
            }).Data.Include(x=>x.Client).Include(x => x.Packages).SingleOrDefault();

            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var project = projectService_
            .SearchProjects(new SearchProjectOptions()
            {
                ProjectId = id,
            }).Data.Include(x => x.Client).SingleOrDefault();

            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
    }
}