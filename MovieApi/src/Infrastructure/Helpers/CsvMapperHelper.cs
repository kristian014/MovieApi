using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using MovieApi.Infrastructure.Movies;
using System.Globalization;

namespace MovieApi.Infrastructure.Helpers;
internal class CsvMapperHelper
{
    private readonly ILogger<MovieSeeder> _logger;
    public CsvMapperHelper(ILogger<MovieSeeder> logger)
    {
        _logger = logger;
    }

    public async Task<List<MovieObject>> GetMoviesFromCsvAsync(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                TrimOptions = TrimOptions.Trim,
                MissingFieldFound = null,
                ReadingExceptionOccurred = context =>
                {
                    return false;
                },
            };

            using (StreamReader reader = new StreamReader(filePath))
            using (CsvReader csv = new CsvReader(reader, config))
            {
                List<MovieObject> records = new List<MovieObject>();

                await foreach (var record in csv.GetRecordsAsync<MovieObject>(cancellationToken))
                {
                    records.Add(record);
                }

                return records;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured while trying to read csv file. {ex.Message}");
            throw;
        }
    }
}
