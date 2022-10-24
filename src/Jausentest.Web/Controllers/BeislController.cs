using Jausentest.Core.Models;
using Jausentest.Core.Services;
using Jausentest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jausentest.Core.Interfaces;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BeislDto>>> GetBeisl()
        {
            var result = await _beislService.GetBeislsAsync();
            return Ok(result);
        }


        [HttpGet("{beislId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        
        [HttpGet("{beislId}/ratings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatingsForBeislIdAsync(long beislId)
        {
            var result = await _beislService.GetRatingsForBeislIdAsync(beislId);

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        
        [HttpPost("{beislId}/ratings")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BeislDto>> AddTagToBeisl([FromBody] RatingDto rating, long beislId)
        {
            var _beisl = await _beislService.AddRatingToBeislAsync(rating, beislId);

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpDelete("{beislId}/ratings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BeislDto>> DeleteTagFromBeisl([FromBody] RatingDto rating, long beislId)
        {
            var _beisl = await _beislService.DeleteRatingFromBeislAsync(rating, beislId);

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddOrUpdateBeisl([FromBody] BeislDto beisl)
        {
            var _beisl = await _beislService.AddOrUpdateBeislAsync(beisl);
            return Created($"{HttpContext.Request.Path}/{_beisl.Id}", _beisl);
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddBeisl([FromBody] BeislDto beisl)
        {
            var _beisl = await _beislService.AddBeislAsync(beisl);
            return Created($"{HttpContext.Request.Path}/{_beisl.Id}", _beisl);
        }


    }
}
