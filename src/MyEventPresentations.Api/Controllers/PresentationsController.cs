using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEventPresentations.Domain.Interfaces;

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
        public IEnumerable<Domain.Models.Presentation> Get()
        {
            return _presentationManager.GetPresentations();
        }

        [HttpGet("{id}")]
        public Domain.Models.Presentation Get(int id)
        {
            return _presentationManager.GetPresentation(id);
        }

        [HttpGet("{id}/schedules")]
        public IEnumerable<Domain.Models.ScheduledPresentation> GetScheduledPresentationsForPresentation(int id)
        {
            return _presentationManager.GetScheduledPresentationsForPresentation(id);
        }
    }
}