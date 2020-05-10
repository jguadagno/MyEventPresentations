using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;

namespace MyEventPresentations.Data.Sqlite
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
        
        public int SavePresentation(Presentation presentation)
        {
            if (presentation == null)
            {
                throw new ArgumentNullException(nameof(presentation), "The presentation cannot be null");
            }

            var dbPresentation = _mapper.Map<Sqlite.Models.Presentation>(presentation);
            using (_presentationContext)
            {
                if (dbPresentation.PresentationId == 0)
                {
                    _presentationContext.Presentations.Add(dbPresentation);
                }
                else
                {
                    _presentationContext.Entry(dbPresentation).State = EntityState.Modified;
                }

                return _presentationContext.SaveChanges() != 0 ? dbPresentation.PresentationId : 0;
            }
        }

        public Presentation GetPresentation(int presentationId)
        {
            using (_presentationContext)
            {
                var presentation =
                    _presentationContext.Presentations.FirstOrDefault(p => p.PresentationId == presentationId);
                return _mapper.Map<Presentation>(presentation);
            }
        }

        public IEnumerable<Presentation> GetPresentations()
        {
            using (_presentationContext)
            {
                var presentations = _presentationContext.Presentations;
                return _mapper.Map<List<Presentation>>(presentations);
            }
        }

        public ScheduledPresentation GetScheduledPresentation(int scheduledPresentationId)
        {
            using (_presentationContext)
            {
                var presentation =
                    _presentationContext.ScheduledPresentations.FirstOrDefault(p => p.ScheduledPresentationId == scheduledPresentationId);
                return _mapper.Map<ScheduledPresentation>(presentation);
            }
        }

        public IEnumerable<ScheduledPresentation> GetScheduledPresentationsForPresentation(int presentationId)
        {
            using (_presentationContext)
            {
                var presentations =
                    _presentationContext.ScheduledPresentations.Where(p =>
                        p.Presentation.PresentationId == presentationId);
                return _mapper.Map<List<ScheduledPresentation>>(presentations);
            }
        }
    }
}