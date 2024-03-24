namespace MovieApi.Domain.Entities;
public class Genre : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;

    public Genre Update(string? name)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;

        return this;
    }
}
