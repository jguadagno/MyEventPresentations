using System;
using System.Collections.Generic;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.Domain.Interfaces
{
    public interface IPresentationRepository
    {
        public int SavePresentation(Presentation presentation);
        public Presentation GetPresentation(int presentationId);
        public IEnumerable<Models.Presentation> GetPresentations();
        public ScheduledPresentation GetScheduledPresentation(int scheduledPresentationId);
        public IEnumerable<ScheduledPresentation> GetScheduledPresentationsForPresentation(int presentationId);
    }
}