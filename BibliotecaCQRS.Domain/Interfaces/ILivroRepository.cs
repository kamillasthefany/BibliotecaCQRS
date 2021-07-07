using BibliotecaCQRS.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaCQRS.Domain.Interfaces
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<Livro> GetById(Guid id);
        Task<Livro> GetByName(string titulo);
        Task<IEnumerable<Livro>> GetAll();

        void Add(Livro livro);
        void Update(Livro livro);
        void Remove(Livro livro);
    }
}
