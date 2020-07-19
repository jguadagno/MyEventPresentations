using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyEventPresentations.WebUi.Services;

namespace MyEventPresentations.WebUi.Controllers
{
    public class PresentationsController : Controller
    {
        private readonly IEventPresentationService _eventPresentationService;
        public PresentationsController(IEventPresentationService eventPresentationService)
        {
            _eventPresentationService = eventPresentationService;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var presentations = await _eventPresentationService.GetPresentationsAsync();
            return View(presentations.ToList());
        }

        public async Task<IActionResult> Details(int id)
        {
            var presentation = await _eventPresentationService.GetPresentationAsync(id);
            if (presentation == null)
            {
                return View();
            }

            var scheduledPresentations =
                await _eventPresentationService.GetScheduledPresentationsForPresentationAsync(id);

            presentation.ScheduledPresentations = scheduledPresentations.ToList();
            return View(presentation);
        }

        public IActionResult Add()
        {
            return View(new Domain.Models.Presentation());
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Add(Domain.Models.Presentation presentation)
        {
            var savedPresentation = await _eventPresentationService.SavePresentationAsync(presentation);
            return RedirectToAction("Details", new {id = savedPresentation.PresentationId});
        }

        public async Task<IActionResult> Scheduled(int id)
        {
            var scheduledPresentation = await _eventPresentationService.GetScheduledPresentationAsync(id);
            return scheduledPresentation == null ? View() : View(scheduledPresentation);
        }
    }
}