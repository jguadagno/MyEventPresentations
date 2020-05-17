using System;
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
        
        public Presentation SavePresentation(Presentation presentation)
        {
            // Validate the fields
            if (presentation == null)
            {
                throw new ArgumentNullException(nameof(presentation), "The presentation can not be null");
            }

            if (presentation.PresentationId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(presentation),
                    "The presentation id can not be less than 1");
            }

            if (string.IsNullOrEmpty(presentation.Title))
            {
                throw new ArgumentNullException(nameof(presentation.Title), "The Title of the presentation can not be null");
            }

            if (string.IsNullOrEmpty(presentation.Abstract))
            {
                throw new ArgumentNullException(nameof(presentation.Abstract), "The Abstract of the presentation can not be null");    
            }

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

        public bool DeletePresentation(int id)
        {
            return _presentationRepository.DeletePresentation(id);
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