using AutoMapper;
using MyEventPresentations.Data.Sqlite.Models;

namespace MyEventPresentations.Data.Sqlite
{
    public class PresentationProfile: Profile
    {
        public PresentationProfile()
        {
            CreateMap<Domain.Models.Presentation, Presentation>();
            CreateMap<Domain.Models.ScheduledPresentation, ScheduledPresentation>();
            
            CreateMap<Presentation, Domain.Models.Presentation>();
            CreateMap<ScheduledPresentation, Domain.Models.ScheduledPresentation>();
        }
    }
}