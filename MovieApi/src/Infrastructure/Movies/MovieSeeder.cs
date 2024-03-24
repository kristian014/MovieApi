using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.Logging;
using MovieApi.Application.Common.Interfaces;
using MovieApi.Domain.Entities;
using MovieApi.Infrastructure.Helpers;
using MovieApi.Infrastructure.Persistence.Context;
using MovieApi.Infrastructure.Persistence.Initialization;
using System.Linq;
using System.Reflection;

namespace MovieApi.Infrastructure.Movies;

public class MovieObject
{
    public DateTime? Release_Date { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string? Overview { get; set; } = string.Empty;
    public double? Popularity { get; set; }
    public int? Vote_Count { get; set; }
    public double? Vote_Average { get; set; }
    public string? Original_Language { get; set; } = string.Empty;
    public string? Poster_Url { get; set; } = string.Empty;
}

public class GenreObject
{
    public string? Name { get; set; }
}

public class LanguageObject
{
    public string? Name { get; set; }
}

public class MovieSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<MovieSeeder> _logger;

    public MovieSeeder(ISerializerService serializerService, ILogger<MovieSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? moviePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (moviePath == null)
        {
            _logger.LogError("Failed to get movie path.");
            return;
        }

        List<MovieObject> movies = await LoadMoviesFromCsvAsync(moviePath, cancellationToken);
        if (movies.Count == 0)
        {
            _logger.LogWarning("No movies found to process.");
            return;
        }

        await SeedLanguagesAsync(movies, cancellationToken);
        await SeedGenresAsync(movies, cancellationToken);
        await SeedMoviesAsync(movies, cancellationToken);
    }

    private async Task SeedGenresAsync(IEnumerable<MovieObject> movies, CancellationToken cancellationToken)
    {
        HashSet<string> existingGenreNames = new HashSet<string>(
        _db.Genres
       .AsEnumerable()
       .Where(g => g.Name != null)
       .Select(g => g.Name.ToUpper()));

        List<Genre> missingGenres = new List<Genre>();
        HashSet<string> missingGenreNames = new HashSet<string>(); // Tracks unique genre names

        foreach (var movie in movies)
        {
            string? genre = movie.Genre?.ToUpper();
            if (!string.IsNullOrEmpty(genre) && !existingGenreNames.Contains(genre) && missingGenreNames.Add(genre))
            {
                Genre newGenre = new Genre();
                newGenre.Update(genre);
                missingGenres.Add(newGenre);
            }
        }

        if (missingGenres.Count > 0)
        {
            _logger.LogInformation("Seeding genres.");
            await _db.Genres.AddRangeAsync(missingGenres, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded genres.");
        }
    }

    private async Task SeedLanguagesAsync(IEnumerable<MovieObject> movies, CancellationToken cancellationToken)
    {
        // Handle Language Seeding
        HashSet<string> existingLanguageNames = new HashSet<string>(
            _db.Languages
               .AsEnumerable()
               .Where(g => g.Abbreviation != null)
               .Select(g => g.Abbreviation.ToUpper()));

        List<Language> missingLanguages = new List<Language>();
        HashSet<string> missingLanguageNames = new HashSet<string>();
        foreach (var movie in movies)
        {
            string? language = movie.Original_Language?.ToUpper();
            if (!string.IsNullOrEmpty(language) && !existingLanguageNames.Contains(language) && missingLanguageNames.Add(language))
            {
                Language newLanguage = new Language();
                newLanguage.Update(language);
                missingLanguages.Add(newLanguage);
            }
        }

        if (missingLanguages.Count > 0)
        {
            _logger.LogInformation("Seeding languages.");
            await _db.Languages.AddRangeAsync(missingLanguages, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded languages.");
        }
    }

    private async Task SeedMoviesAsync(IEnumerable<MovieObject> movies, CancellationToken cancellationToken)
    {
        Microsoft.EntityFrameworkCore.DbSet<Movie> existingMovies = _db.Movies;
        HashSet<string> existingTitles = new HashSet<string>(existingMovies.Where(x => x.Title != null).Select(x => x.Title.ToUpper()));
        IEnumerable<MovieObject> missingMovies = movies.Where(m => m.Title is not null && !existingTitles.Contains(m.Title.ToUpper()));

        if (missingMovies.Any())
        {
            List<Movie> missingMoviesToUpdate = new List<Movie>();

            _logger.LogInformation("Started to Seed movies.");
            foreach (MovieObject movieObj in missingMovies)
            {
                Genre? genre = _db.Genres.FirstOrDefault(x => x.Name == movieObj.Genre);
                Language? language = _db.Languages.FirstOrDefault(x => x.Abbreviation == movieObj.Original_Language);
                if (language is not null && genre is not null)
                {
                    Movie movie = new Movie();
                    movie.Update(movieObj.Release_Date, movieObj.Title, movieObj.Overview, movieObj.Popularity, movieObj.Vote_Count, movieObj.Vote_Average, movieObj.Poster_Url, language.Id, genre.Id);
                    missingMoviesToUpdate.Add(movie);

                }

            }

            if (missingMoviesToUpdate.Count > 0)
            {
                _logger.LogInformation("Seeding movies.");
                await _db.Movies.AddRangeAsync(missingMoviesToUpdate, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Seeded movies.");
            }
        }
    }

    private async Task<List<MovieObject>> LoadMoviesFromCsvAsync(string moviePath, CancellationToken cancellationToken)
    {
        CsvMapperHelper csvMappingHelper = new CsvMapperHelper(_logger);
        return await csvMappingHelper.GetMoviesFromCsvAsync($"{moviePath}/Movies/Files/CSV/mymoviedb.csv", cancellationToken);
    }
}
