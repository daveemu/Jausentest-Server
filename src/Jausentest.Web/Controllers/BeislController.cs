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

        [HttpPost("{beislId}/tags")]
        public async Task<ActionResult<BeislDto>> AddTagToBeisl([FromBody] TagDto tag, long beislId)
        {
            var _beisl = await _beislService.AddTagToBeislAsync(tag, beislId);

            if (_beisl == null)
            {
                return NotFound(new ProblemDetails()
                {
                    Title = "Beisl not found",
                    Detail = $"Beisl with id={beislId} was not found",
                    Status = 404
                });
            }

            return Created($"{HttpContext.Request.Path}/{beislId}", _beisl);
        }

        [HttpDelete("{beislId}/tags")]
        public async Task<ActionResult<BeislDto>> DeleteTagFromBeisl([FromBody] TagDto tag, long beislId)
        {
            var _beisl = await _beislService.DeleteTagFromBeislAsync(tag, beislId);

            if (_beisl == null)
            {
                return NotFound(new ProblemDetails()
                {
                    Title = "Beisl not found",
                    Detail = $"Beisl with id={beislId} was not found",
                    Status = 404
                });
            }

            return Ok(_beisl);
        }

        [HttpPut]
        public async Task<ActionResult> AddOrUpdateBeisl([FromBody] BeislDto beisl)
        {
            var _beisl = await _beislService.AddOrUpdateBeislAsync(beisl);
            return Created($"{HttpContext.Request.Path}/{_beisl.Id}", _beisl);
        }

        [HttpPost]
        public async Task<ActionResult> AddBeisl([FromBody] BeislDto beisl)
        {
            var _beisl = await _beislService.AddBeislAsync(beisl);
            return Created($"{HttpContext.Request.Path}/{_beisl.Id}", _beisl);
        }


    }
}
