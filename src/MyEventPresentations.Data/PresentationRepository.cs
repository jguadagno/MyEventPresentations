using System.Collections.Generic;
using System.Threading.Tasks;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.Data
{
    public class PresentationRepository: IPresentationRepository
    {
        private readonly IPresentationRepositoryStorage _presentationRepositoryStorage;
        
        public PresentationRepository(IPresentationRepositoryStorage presentationRepositoryStorage)
        {
            _presentationRepositoryStorage = presentationRepositoryStorage;
        }
        
        public Task<Presentation> SavePresentationAsync(Presentation presentation)
        {
            return _presentationRepositoryStorage.SavePresentationAsync(presentation);
        }

        public Task<Presentation> GetPresentationAsync(int presentationId)
        {
            return _presentationRepositoryStorage.GetPresentationAsync(presentationId);
        }

        public Task<IEnumerable<Presentation>> GetPresentationsAsync()
        {
            return _presentationRepositoryStorage.GetPresentationsAsync();
        }

        public Task<bool> DeletePresentationAsync(int presentationId)
        {
            return _presentationRepositoryStorage.DeletePresentationAsync(presentationId);
        }

        public Task<ScheduledPresentation> GetScheduledPresentationAsync(int scheduledPresentationId)
        {
            return _presentationRepositoryStorage.GetScheduledPresentationAsync(scheduledPresentationId);
        }

        public Task<IEnumerable<ScheduledPresentation>> GetScheduledPresentationsForPresentationAsync(int presentationId)
        {
            return _presentationRepositoryStorage.GetScheduledPresentationsForPresentationAsync(presentationId);
        }

        public Task<ScheduledPresentation> SaveScheduledPresentationAsync(ScheduledPresentation scheduledPresentation)
        {
            return _presentationRepositoryStorage.SaveScheduledPresentationAsync(scheduledPresentation);
        }
    }
}