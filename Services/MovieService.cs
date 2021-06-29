using ApiCatalogofilmes.Entities;
using ApiCatalogofilmes.Exceptions;
using ApiCatalogofilmes.InputModel;
using ApiCatalogofilmes.Repository;
using ApiCatalogofilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<List<MovieViewModel>> Obter(int pagina, int quantidade)
        {
            var movie = await _movieRepository.Obter(pagina, quantidade);

            return movie.Select(movie => new MovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Genre = movie.Genre,
                Year = movie.Year,
            }).ToList();
        }
        public async Task<MovieViewModel> Obter(Guid id)
        {
            var movie = await _movieRepository.Obter(id);

            if (movie == null)
                return null;

            return new MovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Genre = movie.Genre,
                Year = movie.Year,
            };
        }
        public async Task<MovieViewModel> Inserir(MovieInputModel movie)
        {
            var entityMovie = await _movieRepository.Obter(movie.Name, movie.Genre, movie.Year);

            if (entityMovie.Count > 0)
                throw new MovieRegisteredException();

            var movieInsert = new Movie
            {
                Id = Guid.NewGuid(),
                Name = movie.Name,
                Genre = movie.Genre,
                Year = movie.Year,
            };
            await _movieRepository.Inserir(movieInsert);

            return new MovieViewModel
            {
                Id = movieInsert.Id,
                Name = movie.Name,
                Genre = movie.Genre,
                Year = movie.Year,
            };
        }
        public async Task Atualizar(Guid id, MovieInputModel movie)
        {
            var entityMovie = await _movieRepository.Obter(id);

            if (entityMovie == null)
             throw new MovieNotRegisteredException();

             entityMovie.Name = movie.Name;
             entityMovie.Genre = movie.Genre;
             entityMovie.Year = movie.Year;

             await _movieRepository.Atualizar(entityMovie);
        }
        public async Task Atualizar(Guid id, int year)
        {
            var entityMovie = await _movieRepository.Obter(id);

            if (entityMovie == null)
                throw new MovieNotRegisteredException();

            entityMovie.Year = year;

            await _movieRepository.Atualizar(entityMovie);
        }
        public async Task Remover(Guid id)
        {
            var movie = await _movieRepository.Obter(id);

            if (movie == null)
                throw new MovieNotRegisteredException();

            await _movieRepository.Remover(id);
        }
        public void Dispose()
        {
            _movieRepository?.Dispose();
        }

    }
};
