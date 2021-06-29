using ApiCatalogofilmes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.Repository
{
    public interface IMovieRepository : IDisposable
    {
        Task<List<Movie>> Obter(int pagina, int quantidade);
        Task<Movie> Obter(Guid id);
        Task<List<Movie>> Obter(string name, string genre, int year);
        Task Inserir(Movie movie);
        Task Atualizar(Movie movie);
        Task Remover(Guid id);
    }
}
