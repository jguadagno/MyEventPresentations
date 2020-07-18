using System;
using System.ComponentModel.DataAnnotations;

namespace MyEventPresentations.Domain.Models
{
    public class ScheduledPresentation
    {
        [Range(0, int.MaxValue)]
        public int ScheduledPresentationId { get; set; }
        [Url]
        public string PresentationUri { get; set; }
        [Url]
        public string VideoStorageUri { get; set; }
        [Url]
        public string VideoUri { get; set; }
        [Range(0, int.MaxValue)]
        public int AttendeeCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RoomName { get; set; }
        public Presentation Presentation { get; set; }
        // TODO: Maybe add Presentation Status: Submitted, Approved, Denied, Delivered?
    }
}