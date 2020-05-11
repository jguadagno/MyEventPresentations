using System;
using System.Collections.Generic;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.Domain.Interfaces
{
    public interface IPresentationRepositoryStorage
    {
        public Presentation SavePresentation(Presentation presentation);
        public Presentation GetPresentation(int presentationId);
        public IEnumerable<Presentation> GetPresentations();
        public ScheduledPresentation GetScheduledPresentation(int scheduledPresentationId);
        public IEnumerable<ScheduledPresentation> GetScheduledPresentationsForPresentation(int presentationId);
        public bool DeletePresentation(int presentationId);
    }
}