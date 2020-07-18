using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PresentationsController : ControllerBase
    {
        private readonly ILogger<PresentationsController> _logger;
        private readonly IPresentationManager _presentationManager;

        public PresentationsController(IPresentationManager presentationManager, ILogger<PresentationsController> logger)
        {
            _logger = logger;
            _presentationManager = presentationManager;
        }

        /// <summary>
        /// Gets all of the presentations
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///     Get /presentations
        /// </remarks>
        /// <returns>A list of Presentations</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<Presentation>> GetAllPresentations()
        {
            return await _presentationManager.GetPresentationsAsync();
        }

        /// <summary>
        /// Gets an individual presentation
        /// </summary>
        /// <remarks>
        /// Sample Request
        ///
        /// GET /presentations/1
        /// 
        /// </remarks>
        /// <returns>An individual presentation</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<Presentation> GetPresentation(int id)
        {
            return _presentationManager.GetPresentationAsync(id);
        }

        /// <summary>
        /// Gets the schedule(s) for a given presentation
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        /// GET /presentations/1/schedules
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet("{id}/schedules")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IEnumerable<ScheduledPresentation>> GetScheduledPresentationsForPresentation(int id)
        {
            return _presentationManager.GetScheduledPresentationsForPresentationAsync(id);
        }

        /// <summary>
        /// Saves a presentation
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        /// POST /presentations
        /// {
        ///
        /// }
        /// </remarks>
        /// <returns>The result of the saving</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult<Presentation>> SavePresentation(Presentation presentation)
        {
            try
            {
                var result = await _presentationManager.SavePresentationAsync(presentation);
                if (result != null)
                {
                    return CreatedAtAction(nameof(GetPresentation), new {id = presentation.PresentationId},
                        presentation);
                }
                return Problem("Failed to insert the presentation");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to insert the presentation");
                Console.WriteLine(e.ToString());
                return Problem("Failed to insert the presentation");
            }
        }

        /// <summary>
        /// Updates an existing presentation
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        /// PUT /presentations/1
        /// {
        ///
        /// }
        /// </remarks>
        /// <returns>The status of the update</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdatePresentation(Presentation presentation)
        {
            try
            {
                var result = await _presentationManager.SavePresentationAsync(presentation);
                if (result != null)
                {
                    return NoContent();
                }
                return Problem("Failed to update the presentation");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem("Failed to update the presentation");
            }
        }
        
        /// <summary>
        /// Deletes a presentation
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        /// DELETE /presentations/1
        /// </remarks>
        /// <returns>The status of the deletion</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> DeletePresentation(int id)
        {
            try
            {
                var deleted = await _presentationManager.DeletePresentationAsync(id);
                if (deleted)
                {
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem("Failed to delete the presentation");
            }
        }
    }
}