using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyEventPresentations.Data.Queueing.Queues;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.BusinessLayer
{
    public class PresentationManager: IPresentationManager
    {
        private readonly IPresentationRepository _presentationRepository;
        private readonly PresentationAddedQueue _presentationAddedQueue;
        private readonly PresentationScheduleAddedQueue _presentationScheduleAddedQueue;

        public PresentationManager(IPresentationRepository presentationRepository,
            PresentationAddedQueue presentationAddedQueue,
            PresentationScheduleAddedQueue presentationScheduleAddedQueue)
        {
            _presentationRepository = presentationRepository;
            _presentationAddedQueue = presentationAddedQueue;
            _presentationScheduleAddedQueue = presentationScheduleAddedQueue;
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
            
            var addedPresentationMessage = new Domain.Models.Messages.Presentations.Added {PresentationId = savedPresentation.PresentationId};
            await _presentationAddedQueue.AddMessageAsync(addedPresentationMessage);

            return savedPresentation;
        }

        public async Task<Presentation> GetPresentationAsync(int presentationId)
        {
            return await _presentationRepository.GetPresentationAsync(presentationId);
        }

        public async Task<IEnumerable<Presentation>> GetPresentationsAsync()
        {
            return await _presentationRepository.GetPresentationsAsync();
        }

        public async Task<bool> DeletePresentationAsync(int id)
        {
            return await _presentationRepository.DeletePresentationAsync(id);
        }

        public async Task<ScheduledPresentation> GetScheduledPresentationAsync(int scheduledPresentationId)
        {
            return await _presentationRepository.GetScheduledPresentationAsync(scheduledPresentationId);
        }

        public async Task<IEnumerable<ScheduledPresentation>> GetScheduledPresentationsForPresentationAsync(int presentationId)
        {
            return await _presentationRepository.GetScheduledPresentationsForPresentationAsync(presentationId);
        }

        public async Task<ScheduledPresentation> SaveScheduledPresentationAsync(ScheduledPresentation scheduledPresentation)
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

            var savedScheduledPresentation =  await _presentationRepository.SaveScheduledPresentationAsync(scheduledPresentation);

            var addedPresentationMessage = new Domain.Models.Messages.Presentations.Added {PresentationId = savedScheduledPresentation.PresentationId};
            await _presentationScheduleAddedQueue.AddMessageAsync(addedPresentationMessage);

            return savedScheduledPresentation;
        }
    }
}