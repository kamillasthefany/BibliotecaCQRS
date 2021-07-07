using BibliotecaCQRS.Domain.Interfaces;
using BibliotecaCQRS.Domain.Models;
using BibliotecaCQRS.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaCQRS.Infra.Data.Repository
{
    public class LivroRepository : ILivroRepository
    {
        protected readonly BibliotecaContext Db;
        protected readonly DbSet<Livro> DbSet;

        public LivroRepository(BibliotecaContext context)
        {
            Db = context;
            DbSet = Db.Set<Livro>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<IEnumerable<Livro>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Livro> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<Livro> GetByName(string titulo)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Titulo == titulo);
        }

        public void Add(Livro livro)
        {
            DbSet.Add(livro);
        }

        public void Remove(Livro livro)
        {
            DbSet.Remove(livro);
        }

        public void Update(Livro livro)
        {
            DbSet.Update(livro);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
