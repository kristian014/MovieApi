using MovieApi.Application.Catalog.Genres.DTOs;
using MovieApi.Application.Catalog.Genres.Specs;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Genres.Requests;
public class SearchGenresRequest : PaginationFilter, IRequest<PaginationResponse<GenreDto>>
{
}

public class SearchGenresRequestHandler : IRequestHandler<SearchGenresRequest, PaginationResponse<GenreDto>>
{
    private readonly IReadRepository<Genre> _repository;

    public SearchGenresRequestHandler(IReadRepository<Genre> repository) => _repository = repository;

    public async Task<PaginationResponse<GenreDto>> Handle(SearchGenresRequest request, CancellationToken cancellationToken)
    {
        GenresBySearchRequestSpec? spec = new GenresBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
