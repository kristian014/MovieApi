namespace MovieApi.Domain.Entities;
public class Language : AuditableEntity, IAggregateRoot
{
    public string Abbreviation { get; private set; } = string.Empty;

    public Language Update(string? abbreviation)
    {
        if (abbreviation is not null && Abbreviation?.Equals(abbreviation) is not true) Abbreviation = abbreviation;

        return this;
    }
}
