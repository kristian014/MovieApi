using MovieApi.Application.Catalog.Movies.DTOs;
using MovieApi.Application.Catalog.Movies.Specs;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Movies.Requests;
public class SearchMoviesRequest : PaginationFilter, IRequest<PaginationResponse<MovieDto>>
{
    public string? Title { get; set; }
    public Guid? GenreId { get; set; }
    public DateTime? ReleaseDate { get; set; }
}

public class SearchMoviesRequestHandler : IRequestHandler<SearchMoviesRequest, PaginationResponse<MovieDto>>
{
    private readonly IReadRepository<Movie> _repository;

    public SearchMoviesRequestHandler(IReadRepository<Movie> repository) => _repository = repository;

    public async Task<PaginationResponse<MovieDto>> Handle(SearchMoviesRequest request, CancellationToken cancellationToken)
    {
        MoviesBySearchRequestSpec? spec = new MoviesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
