namespace MyEventPresentations.WebUi.Models
{
    public interface ISettings
    {
        string ApiRootUri { get; set; }
        string ApiScopeUri { get; set; }
    }
}