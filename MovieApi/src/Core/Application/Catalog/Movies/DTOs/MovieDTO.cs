using MovieApi.Application.Catalog.Genres.DTOs;
using MovieApi.Application.Catalog.Languages.DTOs;

namespace MovieApi.Application.Catalog.Movies.DTOs;
public class MovieDto : BaseEntityDto, IDto
{
    public DateTime ReleaseDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public double Popularity { get; set; }
    public int VoteCount { get; set; }
    public double VoteAverage { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public DefaultIdType LanguageId { get; set; }
    public DefaultIdType GenreId { get; set; }
    public LanguageDTO? Language { get; private set; }
    public GenreDto? Genre { get; private set; }
}
