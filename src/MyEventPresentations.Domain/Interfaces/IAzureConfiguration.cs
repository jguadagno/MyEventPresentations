namespace MyEventPresentations.Domain.Interfaces
{
    public interface IAzureConfiguration
    {
        public string AzureWebJobsStorage { get; set; }
    }
}