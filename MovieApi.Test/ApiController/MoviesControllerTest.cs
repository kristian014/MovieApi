using Mapster;
using MovieApi.Application.Catalog.Movies.DTOs;
using MovieApi.Application.Catalog.Movies.Requests;
using MovieApi.Application.Common.Models;
using MovieApi.Domain.Entities;
using MovieApi.Test.Common.Repository;

namespace MovieApi.Test.ApiController
{
    public class MoviesControllerTest
    {
        private List<MovieDto> _movieDtos;

        [SetUp]
        public void Init()
        {
            _movieDtos = new List<MovieDto>()
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
                    Id = new Guid("b1132de4-443d-44a4-4517-44838aacf701"),
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
        }

        [Test]
        public async Task SearchMoviesReturnsPaginatedListSucessfully()
        {
            // Arrange
            var movies = _movieDtos.Adapt<List<Movie>>();

            //Act
            TestRepository<Movie> movieRepo = new TestRepository<Movie>(movies);

            SearchMoviesRequestHandler handler = new SearchMoviesRequestHandler(movieRepo);
            SearchMoviesRequest searchMovies = new SearchMoviesRequest()
            {
                Title = "The"
            };

            // Assert
            PaginationResponse<MovieDto> result = await handler.Handle(searchMovies, new CancellationToken());
            Assert.That(result.Data.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task SearchMoviesWithInvalidTitleReturnsEmptyResult()
        {
            // Arrange
            var movies = _movieDtos.Adapt<List<Movie>>();
            TestRepository<Movie> movieRepo = new TestRepository<Movie>(movies);
            SearchMoviesRequestHandler handler = new SearchMoviesRequestHandler(movieRepo);
            SearchMoviesRequest searchMovies = new SearchMoviesRequest()
            {
                Title = "invalid Title"
            };

            //Act
            PaginationResponse<MovieDto> result = await handler.Handle(searchMovies, new CancellationToken());

            // Assert
            Assert.That(result.Data.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task SearchMoviesByGenreIdReturnsCorrectResult()
        {
            // Arrange
            var movies = _movieDtos.Adapt<List<Movie>>();
            TestRepository<Movie> movieRepo = new TestRepository<Movie>(movies);

            SearchMoviesRequestHandler handler = new SearchMoviesRequestHandler(movieRepo);
            SearchMoviesRequest searchMovies = new SearchMoviesRequest()
            {
                GenreId = new Guid("b1132de4-113d-11a4-1151-11838aacf701")
            };

            //Act
            PaginationResponse<MovieDto> result = await handler.Handle(searchMovies, new CancellationToken());

            // Assert
            Assert.That(result.Data.Count, Is.EqualTo(4));
        }

        [Test]
        public async Task SearchMoviesByGenreIdAndTitleReturnsCorrectResult()
        {
            // Arrange
            var movies = _movieDtos.Adapt<List<Movie>>();
            TestRepository<Movie> movieRepo = new TestRepository<Movie>(movies);

            SearchMoviesRequestHandler handler = new SearchMoviesRequestHandler(movieRepo);
            SearchMoviesRequest searchMovies = new SearchMoviesRequest()
            {
                Title = "The Commando",
                GenreId = new Guid("b1132de4-113d-11a4-1151-11838aacf701")
            };

            //Act
            PaginationResponse<MovieDto> result = await handler.Handle(searchMovies, new CancellationToken());

            // Assert
            Assert.That(result.Data.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task OrderMoviesByReleaseDateStartWithMostRecentDateOrdering()
        {
            // Arrange
            var movies = _movieDtos.Adapt<List<Movie>>();

            TestRepository<Movie> movieRepo = new TestRepository<Movie>(movies);

            SearchMoviesRequestHandler handler = new SearchMoviesRequestHandler(movieRepo);
            SearchMoviesRequest searchMovies = new SearchMoviesRequest()
            {
                Title = "T",
                GenreId = new Guid("b1132de4-113d-11a4-1151-11838aacf701"),
                OrderBy = new[] { "ReleaseDate asc" }
            };

            //Act
            PaginationResponse<MovieDto> result = await handler.Handle(searchMovies, new CancellationToken());

            // Assert       
            Assert.Multiple(() =>
            {
                Assert.That(result.Data.Count, Is.EqualTo(3));
                Assert.That(result.Data.First().ReleaseDate <= result.Data.Last().ReleaseDate);
            });

        }
    }

}
