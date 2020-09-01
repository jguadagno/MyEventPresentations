using MyEventPresentations.Data.Queueing.Constants;

namespace MyEventPresentations.Data.Queueing.Queues
{
    public class PresentationAddedQueue: QueueRepositoryBase
    {
        public PresentationAddedQueue(string storageConnectionString)
            : base(storageConnectionString, QueueNames.Presentations.Added)
        {
            
        }
    }
}