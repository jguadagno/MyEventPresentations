using Microsoft.EntityFrameworkCore;
using MyEventPresentations.Data.SqlServer.Models;

namespace MyEventPresentations.Data.SqlServer
{
    public class PresentationContext: DbContext
    {
        public PresentationContext(DbContextOptions<PresentationContext> options) : base(options) {}
        
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<ScheduledPresentation> ScheduledPresentations { get; set; }
    }
}