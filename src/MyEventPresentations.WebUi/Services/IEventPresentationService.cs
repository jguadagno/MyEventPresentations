using System.Collections.Generic;
using System.Threading.Tasks;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.WebUi.Services
{
    public interface IEventPresentationService
    {
        public Task<Presentation> SavePresentationAsync(Presentation presentation);
        public Task<Presentation> GetPresentationAsync(int presentationId);
        public Task<IEnumerable<Presentation>> GetPresentationsAsync();
        public Task<ScheduledPresentation> GetScheduledPresentationAsync(int scheduledPresentationId);
        public Task<IEnumerable<ScheduledPresentation>> GetScheduledPresentationsForPresentationAsync(int presentationId);
        public Task<bool> DeletePresentationAsync(int id);
        public Task<ScheduledPresentation> SaveScheduledPresentationAsync(ScheduledPresentation scheduledPresentation);
    }
}