using MovieApi.Application.Catalog.Genres.DTOs;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Catalog.Genres.Specs;
public class GenreByIdSpec : Specification<Genre, GenreDto>, ISingleResultSpecification
{
    public GenreByIdSpec(Guid id)
    {
        Query.Where(p => p.Id == id);
    }
}
