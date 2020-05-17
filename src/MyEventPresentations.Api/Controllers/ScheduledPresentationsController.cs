using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEventPresentations.Domain.Interfaces;

namespace MyEventPresentations.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduledPresentationsController : ControllerBase
    {
        private readonly ILogger<PresentationsController> _logger;
        private readonly IPresentationManager _presentationManager;

        public ScheduledPresentationsController(IPresentationManager presentationManager, ILogger<PresentationsController> logger)
        {
            _logger = logger;
            _presentationManager = presentationManager;
        }
        
        [HttpGet("{id}")]
        public Task<Domain.Models.ScheduledPresentation> Get(int id)
        {
            return _presentationManager.GetScheduledPresentationAsync(id);
        }
        
        [HttpPost]
        public async Task <ActionResult<Domain.Models.ScheduledPresentation>> SaveScheduledPresentation(Domain.Models.ScheduledPresentation scheduledPresentation)
        {
            try
            {
                var result = await _presentationManager.SaveScheduledPresentationAsync(scheduledPresentation);
                if (result != null)
                {
                    return CreatedAtAction(nameof(Get), new {id = scheduledPresentation.ScheduledPresentationId},
                        scheduledPresentation);
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
    }
}