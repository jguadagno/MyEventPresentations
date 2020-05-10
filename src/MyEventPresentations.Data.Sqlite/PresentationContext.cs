using Microsoft.EntityFrameworkCore;
using MyEventPresentations.Data.Sqlite.Models;

namespace MyEventPresentations.Data.Sqlite
{
    public class PresentationContext: DbContext
    {
        public PresentationContext(DbContextOptions<PresentationContext> options) : base(options) {}
        
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<ScheduledPresentation> ScheduledPresentations { get; set; }
    }
}