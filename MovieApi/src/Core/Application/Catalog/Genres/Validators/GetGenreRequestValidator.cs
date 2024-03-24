using MovieApi.Application.Catalog.Genres.Requests;

namespace MovieApi.Application.Catalog.Genres.Validators;
public class GetGenreRequestValidator : CustomValidator<GetGenreRequest>
{
    public GetGenreRequestValidator()
    {
        RuleFor(p => p.Id)
           .NotEmpty()
           .NotEqual(Guid.Empty);
    }
}
