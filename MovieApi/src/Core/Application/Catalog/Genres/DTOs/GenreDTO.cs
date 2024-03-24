namespace MovieApi.Application.Catalog.Genres.DTOs;
public class GenreDto : BaseEntityDto, IDto
{
    public string Name { get; private set; } = string.Empty;
}
