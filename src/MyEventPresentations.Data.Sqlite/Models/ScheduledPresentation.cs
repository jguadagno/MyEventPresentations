using System;

namespace MyEventPresentations.Data.Sqlite.Models
{
    public class ScheduledPresentation
    {
        public int ScheduledPresentationId { get; set; }
        public Presentation Presentation { get; set; }
        public string PresentationUri { get; set; }
        public string VideoStorageUri { get; set; }
        public string VideoUri { get; set; }
        public int AttendeeCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RoomName { get; set; }
        // TODO: Maybe add Presentation Status: Submitted, Approved, Denied, Delivered?
    }
}