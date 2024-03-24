using MovieApi.Application.Catalog.Movies.DTOs;
using MovieApi.Application.Catalog.Movies.Specs;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Movies.Requests;
public class SearchMoviesRequests : PaginationFilter, IRequest<PaginationResponse<MovieDTO>>
{
    public string? Title { get; set; }
    public string? GenreName { get; set; }
    public DateTime? ReleaseDate { get; set; }
}

public class SearchMoviesRequestsHandler : IRequestHandler<SearchMoviesRequests, PaginationResponse<MovieDTO>>
{
    private readonly IReadRepository<Movie> _repository;

    public SearchMoviesRequestsHandler(IReadRepository<Movie> repository) => _repository = repository;

    public async Task<PaginationResponse<MovieDTO>> Handle(SearchMoviesRequests request, CancellationToken cancellationToken)
    {
        MoviesBySearchRequestSpec? spec = new MoviesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
