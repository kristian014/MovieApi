using MovieApi.Application.Catalog.Genres.DTOs;
using MovieApi.Application.Catalog.Genres.Specs;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Genres.Requests;
public class GetGenreRequest : IRequest<GenreDto>
{
    public Guid Id { get; set; }

    public GetGenreRequest(Guid id) => Id = id;
}

public class GetGenreRequestHandler : IRequestHandler<GetGenreRequest, GenreDto>
{
    private readonly IRepository<Genre> _repository;
    private readonly IStringLocalizer<GetGenreRequestHandler> _localizer;

    public GetGenreRequestHandler(IRepository<Genre> repository, IStringLocalizer<GetGenreRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<GenreDto> Handle(GetGenreRequest request, CancellationToken cancellationToken)
    {
        GenreDto? genreDto = await _repository.GetBySpecAsync(
           (ISpecification<Genre, GenreDto>)new GenreByIdSpec(request.Id), cancellationToken);

        _ = genreDto ?? throw new NotFoundException(string.Format(_localizer["genre.notfound"], request.Id));
        return genreDto;
    }
}