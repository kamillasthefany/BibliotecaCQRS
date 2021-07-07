using BibliotecaCQRS.Domain.Core.Events;
using BibliotecaCQRS.Infra.Data.Repository.EventSourcing;
using NetDevPack.Identity.User;
using NetDevPack.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaCQRS.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IAspNetUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IAspNetUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);
            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Name ?? _user.GetUserEmail());

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
