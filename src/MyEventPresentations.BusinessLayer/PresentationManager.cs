using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Queues;
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
        
        public async Task<Presentation> SavePresentationAsync(Presentation presentation)
        {
            // Validate the fields
            if (presentation == null)
            {
                throw new ArgumentNullException(nameof(presentation), "The presentation can not be null");
            }

            if (presentation.PresentationId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(presentation),
                    "The presentation id can not be less than 0");
            }

            if (string.IsNullOrEmpty(presentation.Title))
            {
                throw new ArgumentNullException(nameof(presentation.Title), "The Title of the presentation can not be null");
            }

            if (string.IsNullOrEmpty(presentation.Abstract))
            {
                throw new ArgumentNullException(nameof(presentation.Abstract), "The Abstract of the presentation can not be null");    
            }

            var savedPresentation = await _presentationRepository.SavePresentationAsync(presentation);
            
            // TODO: Use the message
            var addedPresentationMessage = new Domain.Models.Messages.Presentations.Added {PresentationId = savedPresentation.PresentationId};
            
            var queueClient = new QueueClient("UseDevelopmentStorage=true", Domain.Constants.QueueNames.Presentations.Added);
            await queueClient.SendMessageAsync(savedPresentation.PresentationId.ToString());

            return savedPresentation;
        }

        public Task<Presentation> GetPresentationAsync(int presentationId)
        {
            return _presentationRepository.GetPresentationAsync(presentationId);
        }

        public Task<IEnumerable<Presentation>> GetPresentationsAsync()
        {
            return _presentationRepository.GetPresentationsAsync();
        }

        public Task<bool> DeletePresentationAsync(int id)
        {
            return _presentationRepository.DeletePresentationAsync(id);
        }

        public Task<ScheduledPresentation> GetScheduledPresentationAsync(int scheduledPresentationId)
        {
            return _presentationRepository.GetScheduledPresentationAsync(scheduledPresentationId);
        }

        public Task<IEnumerable<ScheduledPresentation>> GetScheduledPresentationsForPresentationAsync(int presentationId)
        {
            return _presentationRepository.GetScheduledPresentationsForPresentationAsync(presentationId);
        }

        public Task<ScheduledPresentation> SaveScheduledPresentationAsync(ScheduledPresentation scheduledPresentation)
        {
            // Validate the fields
            if (scheduledPresentation == null)
            {
                throw new ArgumentNullException(nameof(scheduledPresentation), "The scheduled presentation can not be null");
            }

            if (scheduledPresentation.Presentation == null)
            {
                throw new ArgumentNullException(nameof(scheduledPresentation.Presentation), "The presentation can not be null");
            }
            
            // Rules validation
            if (scheduledPresentation.StartTime > scheduledPresentation.EndTime)
            {
                throw new ArgumentOutOfRangeException(nameof(scheduledPresentation.StartTime),
                    scheduledPresentation.StartTime,
                    "The start time of the presentation can not be greater then the end time");
            }

            return _presentationRepository.SaveScheduledPresentationAsync(scheduledPresentation);

        }
    }
}