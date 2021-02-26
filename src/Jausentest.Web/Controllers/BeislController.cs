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
        private readonly IBeislService _beislService;
        public BeislController(IBeislService beislService)
        {
            _beislService = beislService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeislDto>>> GetBeisl()
        {
            var result = await _beislService.GetBeislsAsync();

            if (result == null)
                return NotFound(new ProblemDetails()
                {
                    Title = "No beisl found",
                    Detail = "Can't find any stored Beisl",
                    Status = 404
                });

            return Ok(result);
        }


        [HttpGet("{beislId}")]
        public async Task<ActionResult<BeislDto>> GetBeislById(long beislId)
        {
            var result = await _beislService.GetBeislByIdAsync(beislId);

            if(result == null)
            {
                return NotFound(new ProblemDetails()
                {
                    Title = "Beisl not found",
                    Detail = $"Beisl with id={beislId} was not found",
                    Status = 404
                });
            }

            return Ok(result);
        }


        [HttpGet("{beislId}/tags")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetTagsForBeislIdAsync(long beislId)
        {
            var result = await _beislService.GetTagsForBeislIdAsync(beislId);

            if (result == null)
            {
                return NotFound(new ProblemDetails()
                {
                    Title = "Beisl not found",
                    Detail = $"Beisl with id={beislId} was not found",
                    Status = 404
                });
            }

            return Ok(result);

        }

        [HttpPost]
        public async Task<ActionResult> AddBeisl([FromBody] BeislDto b)
        {
            var _b = await _beislService.AddBeislAsync(b);
            return Created($"{HttpContext.Request.Path}/{_b.Id}", _b);
        }


    }
}
