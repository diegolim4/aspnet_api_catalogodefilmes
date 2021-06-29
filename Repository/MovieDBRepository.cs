using ApiCatalogofilmes.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace ApiCatalogofilmes.Repository
{
    public class MovieDBRepository : IMovieRepository 
    {
        private readonly SqlConnection sqlConnection;

        public MovieDBRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Movie>> Obter(int pagina, int quantidade)
        {
            var movies = new List<Movie>();

            var comando = $"select * from Mvies order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                movies.Add(new Movie
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Genre = (string)sqlDataReader["Genre"],
                    Year = (int)sqlDataReader["year"]
                });
            }

            await sqlConnection.CloseAsync();

            return movies;
        }

        public async Task<Movie> Obter(Guid id)
        {
            Movie movie = null;

            var comando = $"select * from Movies where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                movie = new Movie
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Genre = (string)sqlDataReader["Genre"],
                    Year = (int)sqlDataReader["year"]
                };
            }

            await sqlConnection.CloseAsync();

            return movie;
        }
        public async Task<List<Movie>> Obter(string name, string genre, int year)
        {
            var movies = new List<Movie>();

            var comando = $"select * from Movies where Name = '{name}', Genre = '{genre}' and year = {year} ";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                movies.Add(new Movie
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Genre = (string)sqlDataReader["Genre"],
                    Year = (int)sqlDataReader["year"]
                });
            }
            await sqlConnection.CloseAsync();

            return movies;
        }

        public async Task Inserir(Movie movie)
        {
            var comando = $"insert Movies (Id, Name, Genre, Year) values ('{movie.Id}', '{movie.Name}', '{movie.Genre}', {movie.Year})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Movie movie)
        {
            var comando = $"update Movies (Id, Name, Genre, Year) values ('{movie.Id}', '{movie.Name}', '{movie.Genre}', {movie.Year})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public async Task Remover(Guid id)
        {
            var comando = $"delete from Movies where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

    }
}
