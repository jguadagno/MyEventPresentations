using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyEventPresentations.Domain.Models
{
    public class Presentation
    {
        [Range(0, int.MaxValue)]
        public int PresentationId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Abstract { get; set; }
        [Url]
        public string MoreInfoUri { get; set; }
        [Url]
        public string SourceCodeRepositoryUri { get; set; }
        [Url]
        public string PowerpointUri { get; set; }
        [Url]
        public string VideoUri { get; set; }
        public List<ScheduledPresentation> ScheduledPresentations { get; set; }
    }
}