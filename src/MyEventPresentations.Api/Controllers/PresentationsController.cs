using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IEnumerable<Domain.Models.Presentation>> GetAllPresentations()
        {
            return await _presentationManager.GetPresentationsAsync();
        }

        [HttpGet("{id}")]
        public Task<Presentation> GetPresentation(int id)
        {
            return _presentationManager.GetPresentationAsync(id);
        }

        [HttpGet("{id}/schedules")]
        public Task<IEnumerable<ScheduledPresentation>> GetScheduledPresentationsForPresentation(int id)
        {
            return _presentationManager.GetScheduledPresentationsForPresentationAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Domain.Models.Presentation>> SavePresentation(Domain.Models.Presentation presentation)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePresentation(Presentation presentation)
        {
            try
            {
                var result = await _presentationManager.SavePresentationAsync(presentation);
                if (result != null)
                {
                    return NoContent();
                }
                else
                {
                    return Problem("Failed to update the presentation");
                }
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem("Failed to update the presentation");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePresentation(int id)
        {
            try
            {
                var deleted = await _presentationManager.DeletePresentationAsync(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem("Failed to delete the presentation");
            }
        }
    }
}