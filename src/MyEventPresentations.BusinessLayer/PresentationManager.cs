using System.Collections.Generic;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.BusinessLayer
{
    public class PresentationManager: IPresentationManager
    {
        private readonly IPresentationRepository _presentationRepository;

        public PresentationManager(IPresentationRepository presentationRepository)
        {
            _presentationRepository = presentationRepository;
        }
        
        public int SavePresentation(Presentation presentation)
        {
            return _presentationRepository.SavePresentation(presentation);
        }

        public Presentation GetPresentation(int presentationId)
        {
            return _presentationRepository.GetPresentation(presentationId);
        }

        public IEnumerable<Presentation> GetPresentations()
        {
            return _presentationRepository.GetPresentations();
        }

        public ScheduledPresentation GetScheduledPresentation(int scheduledPresentationId)
        {
            return _presentationRepository.GetScheduledPresentation(scheduledPresentationId);
        }

        public IEnumerable<ScheduledPresentation> GetScheduledPresentationsForPresentation(int presentationId)
        {
            return _presentationRepository.GetScheduledPresentationsForPresentation(presentationId);
        }
    }
}