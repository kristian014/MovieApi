using MovieApi.Application.Catalog.Genres.DTOs;
using MovieApi.Application.Catalog.Genres.Requests;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Genres.Specs;
public class GenresBySearchRequestSpec : EntitiesByPaginationFilterSpec<Genre, GenreDto>
{
    public GenresBySearchRequestSpec(SearchGenresRequest request)
        : base(request)
    {
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            string key = request.Keyword.ToUpper();
            Query.Where(x => x.Name.ToUpper().Contains(key));
        }

        Query.OrderBy(c => c.Name, !request.HasOrderBy());
    }
}
