using MovieApi.Test.Common.Repository;
using MovieApi.Application.Catalog.Movies.DTOs;
using MovieApi.Application.Common.Models;
using MovieApi.Domain.Entities;

namespace MovieApi.Test.ApiController
{
    public class MoviesControllerTest
    {
        [Test]
        public async Task GetPaginatedMoviesSucessfully()
        {
            // Arrange
            var movies = new List<MovieDto>()
            {
                new MovieDto
                {
                    Id = new Guid("b1132de4-113d-11a4-1151-11838aacf701"),
                    ReleaseDate = new DateTime(1990, 09, 30),
                    Title = "Spider-Man: No Way Home",
                    Popularity = 5083.954,
                    VoteAverage = 8.3,
                    VoteCount = 8940,
                    GenreId = new Guid("b1132de4-113d-11a4-1151-11838aacf701"),
                    PosterUrl = "https://image.tmdb.org/t/p/original/1g0dhYtq4irTY1GPXvft6k4YLjm.jpg"
                },
                new MovieDto
                {
                    Id = new Guid("b1132de4-223d-22a4-2251-22838aacf701"),
                    ReleaseDate = new DateTime(2022, 03, 01),
                    Title = "The Batman",
                    Popularity = 4583.954,
                    VoteAverage = 6.3,
                    VoteCount = 89840,
                    GenreId = new Guid("b1132de4-153d-16a4-1651-16838aacf701"),
                    PosterUrl = "https://image.tmdb.org/t/p/original/1g0dhYtq4irTY1GPXvft6k4YLjm.jpg"
                },
                new MovieDto
                {
                    Id = new Guid("b1132de4-333d-33a4-3351-33838aacf701"),
                    ReleaseDate = new DateTime(2022, 02, 25),
                    Title = "No Exit",
                    Popularity = 5553.954,
                    VoteAverage = 9.3,
                    VoteCount = 7640,
                    GenreId = new Guid("b1132de4-113d-11a4-1151-11838aacf701"),
                    PosterUrl = "https://image.tmdb.org/t/p/original/1g0dhYtq4irTY1GPXvft6k4YLjm.jpg"
                },
                new MovieDto
                {
                    Id = new Guid("b1132de4-443d-44a4-451-44838aacf701"),
                    ReleaseDate = new DateTime(2022, 07, 07),
                    Title = "The Commando",
                    Popularity = 556673.954,
                    VoteAverage = 9.3,
                    VoteCount = 8768330,
                    GenreId = new Guid("b1132de4-113d-11a4-1151-11838aacf701"),
                    PosterUrl = "https://image.tmdb.org/t/p/original/1g0dhYtq4irTY1GPXvft6k4YLjm.jpg"
                },
                new MovieDto
                {
                    Id = new Guid("b1132de4-553d-55a4-5551-55838aacf701"),
                    ReleaseDate = new DateTime(2001, 09, 30),
                    Title = "American Pie 2",
                    Popularity = 5083.954,
                    VoteAverage = 8.3,
                    VoteCount = 8940,
                    GenreId = new Guid("b1132de4-113d-11a4-1151-11838aacf701"),
                    PosterUrl = "https://image.tmdb.org/t/p/original/1g0dhYtq4irTY1GPXvft6k4YLjm.jpg"
                },
            };

            //Act
            TestRepository<Movie> movieRepo = new TestRepository<Movie>(movies);

            SearchPatientsRequestHandler handler = new SearchPatientsRequestHandler(patientRepo);
            SearchPatientsRequest searchpatients = new()
            {
                DateOfBirth = new DateTime(1988, 09, 30),
                Initials = "Te"
            };

            // Assert
            PaginationResponse<PatientDto> result = await handler.Handle(searchpatients, new CancellationToken());
            Assert.That(result.Data.Count, Is.EqualTo(2));
        }
    }
}
