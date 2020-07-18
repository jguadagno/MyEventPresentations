using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.Data.SqlServer
{
    public class PresentationRepositoryStorage: IPresentationRepositoryStorage
    {
        private readonly PresentationContext _presentationContext;
        private readonly IMapper _mapper;
        
        public PresentationRepositoryStorage(PresentationContext presentationContext, IMapper mapper)
        {
            _presentationContext = presentationContext;
            _mapper = mapper;
        }
        
        public async Task<Presentation> SavePresentationAsync(Presentation presentation)
        {
            if (presentation == null)
            {
                throw new ArgumentNullException(nameof(presentation), "The presentation cannot be null");
            }

            var dbPresentation = _mapper.Map<SqlServer.Models.Presentation>(presentation);
            await using (_presentationContext)
            {
                if (dbPresentation.PresentationId == 0)
                {
                    await _presentationContext.Presentations.AddAsync(dbPresentation);
                }
                else
                {
                    _presentationContext.Entry(dbPresentation).State = EntityState.Modified;
                }

                var result = await _presentationContext.SaveChangesAsync();

                if (result > 0)
                {
                    // This is because the presentation object does not have the PresentationId and
                    // we need to get this from EF
                    presentation.PresentationId = dbPresentation.PresentationId;
                }
                
                return result != 0 ? presentation : null;
            }
        }

        public async Task<Presentation> GetPresentationAsync(int presentationId)
        {
            await using (_presentationContext)
            {
                var presentation = 
                    _presentationContext.Presentations.FirstOrDefaultAsync(p => p.PresentationId == presentationId);
                return _mapper.Map<Presentation>(presentation);
            }
        }

        public async Task<IEnumerable<Presentation>> GetPresentationsAsync()
        {
            await using (_presentationContext)
            {
                var presentations = _presentationContext.Presentations.AsAsyncEnumerable();
                return _mapper.Map<List<Presentation>>(presentations);
            }
        }

        public async Task<bool> DeletePresentationAsync(int presentationId)
        {
            await using (_presentationContext)
            {
                var dbPresentation = await _presentationContext.Presentations.FindAsync(presentationId);
                if (dbPresentation == null)
                {
                    return false;
                }

                _presentationContext.Presentations.Remove(dbPresentation);
                return await _presentationContext.SaveChangesAsync() != 0;
            }
        }

        public async Task<ScheduledPresentation> GetScheduledPresentationAsync(int scheduledPresentationId)
        {
            await using (_presentationContext)
            {
                var presentation = _presentationContext.ScheduledPresentations
                    .FirstOrDefaultAsync(p => p.ScheduledPresentationId == scheduledPresentationId);
                return _mapper.Map<ScheduledPresentation>(presentation);
            }
        }

        public async Task<IEnumerable<ScheduledPresentation>> GetScheduledPresentationsForPresentationAsync(int presentationId)
        {
            await using (_presentationContext)
            {
                var presentations =
                    _presentationContext.ScheduledPresentations
                        .Where(p => p.Presentation.PresentationId == presentationId).ToListAsync();
                return _mapper.Map<List<ScheduledPresentation>>(presentations);
            }
        }

        public async Task<ScheduledPresentation> SaveScheduledPresentationAsync(ScheduledPresentation scheduledPresentation)
        {
            if (scheduledPresentation == null)
            {
                throw new ArgumentNullException(nameof(scheduledPresentation), "The scheduled presentation can not be null");
            }

            if (scheduledPresentation.Presentation == null)
            {
                throw new ArgumentNullException(nameof(scheduledPresentation.Presentation), "The presentation can not be null");
            }
            
            var dbScheduledPresentation = _mapper.Map<SqlServer.Models.ScheduledPresentation>(scheduledPresentation);

            await using (_presentationContext)
            {
                if (scheduledPresentation.ScheduledPresentationId == 0)
                {
                    var presentation = await _presentationContext.Presentations.FirstOrDefaultAsync(p =>
                        p.PresentationId == scheduledPresentation.Presentation.PresentationId);
                    if (presentation == null)
                    {
                        // The specified Presentation was not found
                        throw new ApplicationException("The presentation was not found.");
                    }

                    if (presentation.ScheduledPresentations == null)
                    {
                        presentation.ScheduledPresentations = new List<Models.ScheduledPresentation> {dbScheduledPresentation};
                    }
                    else
                    {
                        presentation.ScheduledPresentations.Add(dbScheduledPresentation);
                    }

                }
                else
                {
                    _presentationContext.Entry(dbScheduledPresentation).State = EntityState.Modified;
                }

                var result = await _presentationContext.SaveChangesAsync();

                if (result > 0)
                {
                    // This is because the scheduled presentation object does not have the ScheduledPresentationId and
                    // we need to get this from EF
                    scheduledPresentation.ScheduledPresentationId = dbScheduledPresentation.ScheduledPresentationId;
                }

                return result != 0 ? scheduledPresentation : null;
            }
        }
    }
}