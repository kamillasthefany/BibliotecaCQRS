using BibliotecaCQRS.Domain.Core.Events;
using BibliotecaCQRS.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaCQRS.Infra.Data.Repository.EventSourcing
{
    public class EventStoreSQLRepository : IEventStoreRepository
    {
        private readonly EventStoreSQLContext _context;

        public EventStoreSQLRepository(EventStoreSQLContext context)
        {
            _context = context;
        }

        public async Task<IList<StoredEvent>> All(Guid aggregateId)
        {
            return await (from e in _context.StoredEvent
                          where e.AggregateId == aggregateId select e).ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvent.Add(theEvent);
            _context.SaveChanges();
        }
    }
}
