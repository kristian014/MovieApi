using MovieApi.Application.Catalog.Brands;
using MovieApi.Application.Catalog.Genres.DTOs;
using MovieApi.Application.Catalog.Genres.Requests;

namespace MovieApi.Host.Controllers.Catalog;
public class GenresController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Genres)]
    [OpenApiOperation("Search genres using available filters.", "")]
    public Task<PaginationResponse<GenreDto>> SearchAsync(SearchGenresRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Genres)]
    [OpenApiOperation("Get genre details.", "")]
    public Task<GenreDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetGenreRequest(id));
    }
}
