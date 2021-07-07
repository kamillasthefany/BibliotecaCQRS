using BibliotecaCQRS.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaCQRS.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}
