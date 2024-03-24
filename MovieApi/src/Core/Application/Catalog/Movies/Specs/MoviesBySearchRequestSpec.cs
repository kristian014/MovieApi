using MovieApi.Application.Catalog.Movies.DTOs;
using MovieApi.Application.Catalog.Movies.Requests;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Movies.Specs;
public class MoviesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Movie, MovieDTO>
{
    public MoviesBySearchRequestSpec(SearchMoviesRequests request)
        : base(request)
    {
        Query
            .Include(x => x.Genre)
            .OrderBy(x => x.Title, !request.HasOrderBy());

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            string title = request.Title.ToUpper();
            Query
                .Where(x => x.Title.ToUpper().Contains(title));
        }

        if (!string.IsNullOrWhiteSpace(request.GenreName))
        {
            string genreName = request.GenreName.ToUpper();
            Query
                .Where(x => x.Genre != null && x.Genre.Name.ToUpper().Contains(genreName));
        }
    }
}

////{
////  "pageNumber": 0,
////  "pageSize": 0,
////  "orderBy": [
////    "ReleaseDate",
////   "Title"
////  ],
////  "title": "the f",
////  "genreName": "action",
////   "releaseDate": "2020-10-13"
////}