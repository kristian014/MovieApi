using MovieApi.Application.Catalog.Movies.DTOs;
using MovieApi.Application.Catalog.Movies.Requests;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Movies.Specs;
public class MoviesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Movie, MovieDto>
{
    public MoviesBySearchRequestSpec(SearchMoviesRequest request)
        : base(request)
    {
        Query
            .Include(x => x.Genre)
            .Include(x => x.Language)
            .OrderByDescending(x => x.ReleaseDate, !request.HasOrderBy());

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            string title = request.Title.ToUpper();
            Query
                .Where(x => x.Title.ToUpper().Contains(title));
        }

        if (request.GenreId.HasValue)
        {
            Query
              .Where(x => x.GenreId.Equals(request.GenreId.Value));
        }
    }
}