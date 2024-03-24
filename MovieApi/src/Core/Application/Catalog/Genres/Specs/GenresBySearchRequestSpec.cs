using MovieApi.Application.Catalog.Genres.DTOs;
using MovieApi.Application.Catalog.Genres.Requests;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Genres.Specs;
public class GenresBySearchRequestSpec : EntitiesByPaginationFilterSpec<Genre, GenreDto>
{
    public GenresBySearchRequestSpec(SearchGenresRequest request)
        : base(request)
    {
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
    }
}
