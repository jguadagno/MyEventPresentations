namespace MyEventPresentations.WebUi.Models
{
    public class Settings : ISettings
    {
        public string ApiRootUri { get; set; }
        public string ApiScopeUri { get; set; }
    }
}