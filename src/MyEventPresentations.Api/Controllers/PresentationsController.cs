using System;
using System.Collections.Generic;
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
        public IEnumerable<Domain.Models.Presentation> GetAllPresentations()
        {
            return _presentationManager.GetPresentations();
        }

        [HttpGet("{id}")]
        public Domain.Models.Presentation GetPresentation(int id)
        {
            return _presentationManager.GetPresentation(id);
        }

        [HttpGet("{id}/schedules")]
        public IEnumerable<Domain.Models.ScheduledPresentation> GetScheduledPresentationsForPresentation(int id)
        {
            return _presentationManager.GetScheduledPresentationsForPresentation(id);
        }

        [HttpPost]
        public ActionResult<Domain.Models.Presentation> SavePresentation(Domain.Models.Presentation presentation)
        {
            try
            {
                var result = _presentationManager.SavePresentation(presentation);
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
        public IActionResult UpdatePresentation(Domain.Models.Presentation presentation)
        {
            try
            {
                var result = _presentationManager.SavePresentation(presentation);
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
        public ActionResult DeletePresentation(int id)
        {
            try
            {
                var deleted = _presentationManager.DeletePresentation(id);
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