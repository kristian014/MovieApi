using MovieApi.Application.Catalog.Movies.DTOs;
using MovieApi.Application.Catalog.Movies.Requests;

namespace MovieApi.Host.Controllers.Catalog;
public class MoviesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Search movies using available filters.", "")]
    public Task<PaginationResponse<MovieDto>> SearchAsync(SearchMoviesRequest request)
    {
        return Mediator.Send(request);
    }
}
