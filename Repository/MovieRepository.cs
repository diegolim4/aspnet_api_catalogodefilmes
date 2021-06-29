using ApiCatalogofilmes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private static Dictionary<Guid, Movie> movies = new Dictionary<Guid, Movie>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Movie{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Star Wars - O Império Contra-Ataca", Genre = "Sci-Fi", Year = 1980} },
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Movie{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "O senhor dos Anéis", Genre = "Fantasy", Year = 2000} },

        };

        public Task<List<Movie>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(movies.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList()); 
        }

        public Task<Movie> Obter(Guid id)
        {
            if (!movies.ContainsKey(id))
                return null;

            return Task.FromResult(movies[id]);
        }

        public Task<List<Movie>> Obter(string name, string genre, int year)
        {
            return Task.FromResult(movies.Values.Where(movie => movie.Name.Equals(name) && movie.Genre.Equals(genre)).ToList());
        }

        public Task<List<Movie>> ObterSemLambda(string name, string genre)
        {
            var retorno = new List<Movie>();

            foreach (var movie in movies.Values)
            {
                if (movie.Name.Equals(name) && movie.Genre.Equals(genre))
                    retorno.Add(movie);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Movie movie)
        {
            movies.Add(movie.Id ,movie);
            return Task.CompletedTask;
        }

        public Task Atualizar(Movie jogo)
        {
            movies[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            movies.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }

    }
}
