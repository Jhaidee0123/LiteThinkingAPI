using Microsoft.AspNetCore.Identity;

namespace LiteThinking.Domain.Entities.Users;

public class User : IdentityUser
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
