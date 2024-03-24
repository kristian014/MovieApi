namespace MovieApi.Domain.Common.Contracts;
public abstract class BaseEntityDto
{
    public Guid Id { get; set; } = default!;
}
