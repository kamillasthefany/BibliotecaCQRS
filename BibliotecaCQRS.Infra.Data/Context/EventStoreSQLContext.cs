using BibliotecaCQRS.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaCQRS.Infra.Data.Context
{
    public class EventStoreSQLContext : DbContext
    {
        public EventStoreSQLContext(DbContextOptions<EventStoreSQLContext> options) : base(options) { }
        public DbSet<StoredEvent> StoredEvent { get; set; }
    }
}
