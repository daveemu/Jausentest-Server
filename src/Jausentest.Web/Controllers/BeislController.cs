using Jausentest.Core.Models;
using Jausentest.Core.Services;
using Jausentest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jausentest.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeislController : ControllerBase
    {

        public BeislController(IBeislService beislService)
        {
            BeislService = beislService;
        }

        public IBeislService BeislService { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeislDto>>> GetBeisl()
        {
            var result = await BeislService.GetBeisls();

            if (result == null)
                return NotFound(new ProblemDetails()
                {
                    Title = "No beisl found",
                    Detail = "Can't find any stored Beisl information.",
                    Status = 404,
                    Type = "https://http.cat/404"
                });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddBeisl([FromBody] BeislEntity b)
        {
            var _b = await BeislService.AddBeisl(b);
            return Created($"{HttpContext.Request.Path}/{_b.Id}", _b);
        }


    }
}
