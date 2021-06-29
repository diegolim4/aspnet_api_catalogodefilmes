using ApiCatalogofilmes.InputModel;
using ApiCatalogofilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.Services
{
    public interface IMovieService : IDisposable
    {
        Task<List<MovieViewModel>> Obter(int pagina, int quantidade);
        Task<MovieViewModel> Obter(Guid id);
        Task<MovieViewModel> Inserir(MovieInputModel movie);
        Task Atualizar(Guid id, MovieInputModel movie);
        Task Atualizar(Guid id, int year);
        Task Remover(Guid id);
    }
}

