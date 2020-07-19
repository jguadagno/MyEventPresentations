using MyEventPresentations.Domain.Interfaces;

namespace MyEventPresentations.Domain.Models
{
    public class AzureConfiguration: IAzureConfiguration
    {
        public string AzureWebJobsStorage { get; set; }
    }
}