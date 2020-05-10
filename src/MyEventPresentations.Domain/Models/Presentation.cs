using System;
using System.Collections.Generic;

namespace MyEventPresentations.Domain.Models
{
    public class Presentation
    {
        public int PresentationId { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string MoreInfoUri { get; set; }
        public string SourceCodeRepositoryUri { get; set; }
        public string PowerpointUri { get; set; }
        public string VideoUri { get; set; }
        public List<ScheduledPresentation> ScheduledPresentations { get; set; }
    }
}