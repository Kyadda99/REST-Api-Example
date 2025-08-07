using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers
{
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpPost(ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            var movie = request.MapToMovie();
            await _movieRepository.CreateAsync(movie);


            return CreatedAtAction(nameof(Get), new { idOrSlug = movie.Id }, movie);
        }

        [HttpGet(ApiEndpoints.Movies.Get)]
        public async Task<IActionResult> GetById([FromRoute] string idOrSlug)
        {
            var movie = Guid.TryParse(idOrSlug, out var id)
                ? await _movieRepository.GetByIdAsync(id)
                : await _movieRepository.GetBySlugAsync(idOrSlug);

            if (movie == null)
            {
                return NotFound();
            }

            var response = movie.MapToResponse();

            return Ok(response);

        }

        [HttpGet(ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> Get()
        {
            var movies = await _movieRepository.GetAllAsync();
            if (movies == null)
            {
                return NotFound();
            }

            var response = movies.MapToResponse();

            return Ok(response);

        }

        [HttpPut(ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest request)
        {
            var movie = request.MapToMovie(id);
            var result = await _movieRepository.UpdateAsync(movie);
            if (!result)
            {
                return NotFound();
            }

            var response = movie.MapToResponse();

            return Ok(response);

        }

        [HttpDelete(ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _movieRepository.DeleteByIdAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();

        }
    }
}
