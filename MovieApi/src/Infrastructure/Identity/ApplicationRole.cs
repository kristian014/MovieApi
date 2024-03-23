using Microsoft.AspNetCore.Identity;

namespace MovieApi.Infrastructure.Identity;
public class ApplicationRole : IdentityRole
{
    public string? Description { get; set; }

    public ApplicationRole(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }
}