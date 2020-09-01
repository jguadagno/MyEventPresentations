using MyEventPresentations.Data.Queueing.Constants;

namespace MyEventPresentations.Data.Queueing.Queues
{
    public class PresentationScheduleAddedQueue : QueueRepositoryBase
    {
        public PresentationScheduleAddedQueue(string storageConnectionString)
            : base(storageConnectionString, QueueNames.Presentations.ScheduleAdded)
        {
            
        }
    }
}