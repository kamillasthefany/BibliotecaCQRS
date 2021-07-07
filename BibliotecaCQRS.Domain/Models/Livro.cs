using System;
using NetDevPack.Domain;

namespace BibliotecaCQRS.Domain.Models
{
    public class Livro : Entity, IAggregateRoot
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        protected Livro() { }

        public Livro(Guid id, string titulo, string autor)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
        }
    }
}
