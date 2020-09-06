using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using CrowdFundT2.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CrowdFundT2.Web.Controllers
{
    [Route("Client")]
    public class ClientController : Controller
    {
        private IClientService clientService_;

        public ClientController(IClientService clientService)
        {
            clientService_ = clientService;
        }

        [HttpGet]
        public IActionResult Client()
        {
            var clients = clientService_.SearchClient(new SearchClientOptions()).Data
                .Include(c => c.Projects)
                .Include(c => c.InvestedProjects)
                .ToList();

            var cflist = new CfModel()
            {
                Client = clients
            };

            return View(cflist);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateClientOptions options)
        {
            var result = clientService_.CreateClient(options);

            if(!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(new{  
                    clientId=result.Data.ClientId
            });
        }

        [HttpGet("Create")]
        public IActionResult CreateNewClient()
        {
            return View();
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var client = clientService_
                .SearchClient(new SearchClientOptions()
                {
                    ClientId = id,

                }).Data
                .SingleOrDefault();

            if (client == null)
            {
                return NotFound();
            }
            
            return View(client);
        }

        [HttpPatch("Update/{id}")]
        public IActionResult Update(int id,[FromBody] UpdateClientOptions options)
        {
            var result = clientService_.UpdateClient(id, options);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }

            return Json(result);
        }
         
        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            var result = clientService_.DeleteClient(id);

            if(!result.Success)
            {
                return StatusCode((int)result.ErrorCode,
                    result.ErrorText);
            }
            return Json(result);
        }

        [Route("Search")]
        [HttpGet]
        public IActionResult Search(SearchClientOptions options)
        {
            var result = clientService_
                .SearchClient(options)
                .Data.ToList();

            return Json(result);
        }


        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var client = clientService_
                .SearchClient(new SearchClientOptions()
                {
                    ClientId = id,

                }).Data
                .Include(c => c.Projects)
                .Include(c => c.InvestedProjects)
                .SingleOrDefault();

            if (client==null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var client = clientService_
                .SearchClient(new SearchClientOptions()
                {
                    ClientId = id,

                }).Data.SingleOrDefault();

            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

    }
}