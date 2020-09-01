using JosephGuadagno.AzureHelpers.Storage;
using MyEventPresentations.Data.Queueing.Interfaces;

namespace MyEventPresentations.Data.Queueing
{
    public class QueueRepositoryBase : Queue, IQueueRepository
    {
        protected QueueRepositoryBase(string storageConnectionString, string queueName) : 
            base(storageConnectionString, queueName)
        {
            
        }
    }
}