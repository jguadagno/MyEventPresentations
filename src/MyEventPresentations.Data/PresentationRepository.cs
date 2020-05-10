using System;
using System.Collections.Generic;
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
        
        public Presentation SavePresentation(Presentation presentation)
        {
            return _presentationRepositoryStorage.SavePresentation(presentation);
        }

        public Presentation GetPresentation(int presentationId)
        {
            return _presentationRepositoryStorage.GetPresentation(presentationId);
        }

        public IEnumerable<Presentation> GetPresentations()
        {
            return _presentationRepositoryStorage.GetPresentations();
        }

        public ScheduledPresentation GetScheduledPresentation(int scheduledPresentationId)
        {
            return _presentationRepositoryStorage.GetScheduledPresentation(scheduledPresentationId);
        }

        public IEnumerable<ScheduledPresentation> GetScheduledPresentationsForPresentation(int presentationId)
        {
            return _presentationRepositoryStorage.GetScheduledPresentationsForPresentation(presentationId);
        }
    }
}