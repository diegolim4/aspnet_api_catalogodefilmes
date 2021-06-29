using ApiCatalogofilmes.Exceptions;
using ApiCatalogofilmes.InputModel;
using ApiCatalogofilmes.Services;
using ApiCatalogofilmes.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogofilmes.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService jogoService)
        {
            _movieService = jogoService;
        }


        [Route("api/v1/[controller]")]
        [ApiController]
        public class MovieController : ControllerBase
        {
            private readonly IMovieService _movieService;

            public MovieController(IMovieService movieService)
            {
                _movieService = movieService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<MovieViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
            {
                var movies = await _movieService.Obter(pagina, quantidade);

                if (movies.Count() == 0)
                    return NoContent();

                return Ok(movies);
            }

            [HttpGet("{idMovie:guid}")]
            public async Task<ActionResult<List<MovieViewModel>>> Obter([FromRoute] Guid idMovie)
            {
                var movie = await _movieService.Obter(idMovie);

                if (movie == null)
                    return NoContent();

                return Ok(movie);
            }
            
            [HttpPost]
            public async Task<ActionResult<MovieViewModel>> InserirFilme([FromBody] MovieInputModel movieInputModel)
            {
                try
                {
                    var movie = await _movieService.Inserir(movieInputModel);
                     
                    return Ok(movie);
                }
                catch (MovieRegisteredException)
                {
                    return UnprocessableEntity("Já existe um filme com esse nome");
                }
            }
            
            [HttpPut("{idMovie:guid}")]
            public async Task<ActionResult> AtualizarFilme([FromRoute] Guid idMovie, [FromBody] MovieInputModel movieInputModel)
            {
                try
                {
                    await _movieService.Atualizar(idMovie, movieInputModel);

                    return Ok();
                }
                catch (MovieNotRegisteredException)
                {
                    return NotFound("Não existe este Filme");
                }
            }

            [HttpPatch("{idMovie:guid}/year/{year:int}")]
            public async Task<ActionResult> AtualizarFilme([FromRoute] Guid idMovie, [FromRoute] int year)
            {
                try
                {
                    await _movieService.Atualizar(idMovie, year);

                    return Ok();
                }
                catch (MovieNotRegisteredException)
                {
                    return NotFound("Não existe este Filme");
                }
            }
            [HttpDelete("{idMovie:guid}")]
            public async Task<ActionResult> ApagarFilme([FromRoute] Guid idMovie)
            {
                try
                {
                    await _movieService.Remover(idMovie);

                    return Ok();
                }
                catch (MovieNotRegisteredException)
                {
                    return NotFound("Não existe este Filme");
                }
            }
        }   

    }
}
