using Mapster;
using Microsoft.Extensions.Localization;
using Moq;
using MovieApi.Application.Catalog.Genres.DTOs;
using MovieApi.Application.Catalog.Genres.Requests;
using MovieApi.Domain.Entities;
using MovieApi.Test.Common.Repository;

namespace MovieApi.Test.ApiController
{
    public class GenreControllerTest
    {
        private List<GenreDto> _genreDtos;

        [SetUp]
        public void Init()
        {
            _genreDtos = new List<GenreDto>()
            {
                new GenreDto
                {
                    Id = new Guid("b1132de4-113d-11a4-1151-11838aacf701"),
                    Name = "Action",
                },
                new GenreDto
                {
                    Id = new Guid("b1132de4-113d-11a4-1151-11838aacf701"),
                    Name = "Action, Horror",
                },
                new GenreDto
                {
                    Id = new Guid("b1132de4-153d-16a4-1651-16838aacf701"),
                    Name = "Horror",
                },
                 new GenreDto
                {
                    Id = new Guid("b1132de4-143d-16a4-1651-16838aacf701"),
                    Name = "Adventure",
                }
            };
        }

        [Test]
        public async Task SearchGenresByKeywordReturnsCoorectResult()
        {
            // Arrange
            var genres = _genreDtos.Adapt<List<Genre>>();
            TestRepository<Genre> genreRepo = new TestRepository<Genre>(genres);
            SearchGenresRequestHandler handler = new SearchGenresRequestHandler(genreRepo);
            SearchGenresRequest searchGenres = new SearchGenresRequest()
            {
                Keyword = "Action"
            };

            // Act
            var result = await handler.Handle(searchGenres, new CancellationToken());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Data.Count, Is.EqualTo(2));
                Assert.That(result.Data.First().Name, Is.EqualTo("Action"));
            });

        }

        [Test]
        public async Task SearchGenresByPartialNameReturnsCorrectResult()
        {
            // Arrange
            var genres = _genreDtos.Adapt<List<Genre>>();
            TestRepository<Genre> genreRepo = new TestRepository<Genre>(genres);
            SearchGenresRequestHandler handler = new SearchGenresRequestHandler(genreRepo);
            SearchGenresRequest searchGenres = new SearchGenresRequest()
            {
                Keyword = "Act"
            };

            // Act
            var result = await handler.Handle(searchGenres, new CancellationToken());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Data.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Data.Any(g => g.Name.Contains("Act")));
            });

        }

        [Test]
        public async Task SearchForNonExistentGenreReturnsEmptyResult()
        {
            // Arrange
            var genres = _genreDtos.Adapt<List<Genre>>();
            TestRepository<Genre> genreRepo = new TestRepository<Genre>(genres);
            SearchGenresRequestHandler handler = new SearchGenresRequestHandler(genreRepo);
            SearchGenresRequest searchGenres = new SearchGenresRequest()
            {
                Keyword = "invalid search"
            };

            // Act
            var result = await handler.Handle(searchGenres, new CancellationToken());

            // Assert
            Assert.That(result.Data.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetGenreByIdReturnsGenreDetails()
        {
            // Arrange
            var genres = _genreDtos.Adapt<List<Genre>>();
            Guid validGenreId = new Guid("b1132de4-113d-11a4-1151-11838aacf701");
            TestRepository<Genre> genreRepo = new TestRepository<Genre>(genres);
            Mock<IStringLocalizer<GetGenreRequestHandler>>? localizer = new Mock<IStringLocalizer<GetGenreRequestHandler>>();
            LocalizedString? localizedString = new LocalizedString("genre.notfound", "genre.notfound");
            localizer.Setup(x => x["genre.notfound"]).Returns(localizedString);

            GetGenreRequestHandler handler = new GetGenreRequestHandler(genreRepo, localizer.Object);


            GetGenreRequest request = new GetGenreRequest(validGenreId);

            // Act
            var result = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(validGenreId));
            });
        }

    }
}
