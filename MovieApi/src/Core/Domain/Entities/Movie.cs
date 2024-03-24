namespace MovieApi.Domain.Entities;
public class Movie : AuditableEntity, IAggregateRoot
{
    public DateTime ReleaseDate { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Overview { get; private set; } = string.Empty;
    public double Popularity { get; private set; }
    public int VoteCount { get; private set; }
    public double VoteAverage { get; private set; }
    public string PosterUrl { get; private set; } = string.Empty;
    public DefaultIdType LanguageId { get; private set; }
    public DefaultIdType GenreId { get; private set; }

    public virtual Language? Language { get; private set; }

    public virtual Genre? Genre { get; private set; }

    public Movie Update(DateTime? releaseDate, string? title, string? overView, double? popularity, int? voteCount, double? voteAverage, string? posterUrl, Guid? languageId, Guid? genreId)
    {
        if (releaseDate is not null && popularity.Equals(ReleaseDate) is not true) ReleaseDate = (DateTime)releaseDate;
        if (title is not null && title.Equals(Title) is not true) Title = title;
        if (overView is not null && Overview.Equals(Overview) is not true) Overview = Overview;
        if (popularity is not null && popularity.Equals(Popularity) is not true) Popularity = (double)popularity;
        if (voteCount is not null && voteCount.Equals(VoteCount) is not true) VoteCount = (int)voteCount;
        if (voteAverage is not null && voteAverage.Equals(VoteAverage) is not true) VoteAverage = (double)voteAverage;
        if (posterUrl is not null && posterUrl.Equals(PosterUrl) is not true) PosterUrl = PosterUrl;
        if (languageId is not null && languageId.Equals(LanguageId) is not true) LanguageId = (DefaultIdType)languageId;
        if (genreId is not null && genreId.Equals(GenreId) is not true) GenreId = (DefaultIdType)genreId;

        return this;
    }
}
