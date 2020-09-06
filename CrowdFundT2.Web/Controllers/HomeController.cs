using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrowdFundT2.Web.Models;
using CrowdFundT2.Core.Services.Options;
using CrowdFundT2.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using CrowdFundT2.Core.Model;

namespace CrowdFundT2.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProjectService projectService_;
        private IClientService clientService_;


        public HomeController(ILogger<HomeController> logger, IProjectService projectService, IClientService clientService)
        {
            projectService_ = projectService;
            clientService_ = clientService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var projects = projectService_.SearchProjects(new SearchProjectOptions()).Data
                .ToList();


            /* All About Angel Investors */

            var clients = clientService_.SearchClient(new SearchClientOptions()).Data
                .Include(x => x.InvestedProjects).SelectMany(x => x.InvestedProjects
                .OrderByDescending(y=>y.InvestedAmount).Take(1), (x, y) => new { x,y })
                .OrderByDescending(z => z.y.InvestedAmount).Select(x=>x.y).Take(3).ToList();

            var Angels = new List<Client>();

            foreach (var item in clients)
            {
                var AngelClients = clientService_.SearchClient(new SearchClientOptions() 
                { ClientId= item.ClientId}).Data.Include(x=>x.InvestedProjects).SingleOrDefault();
                Angels.Add(AngelClients);
            }

            var projectList = new CfModel()
            {
                Projects = projects,
                Client = Angels
            };
            return View(projectList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
