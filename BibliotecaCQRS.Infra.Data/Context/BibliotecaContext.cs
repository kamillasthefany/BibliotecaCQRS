using BibliotecaCQRS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Domain;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System.Linq;
using System.Threading.Tasks;


namespace BibliotecaCQRS.Infra.Data.Context
{
    public sealed class BibliotecaContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Livro> Livros { get; set; }

        public async Task<bool> Commit()
        {
            await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
            var success = await SaveChangesAsync() > 0;
            return success;
        }
    }
    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }

}
