namespace LiteThinking.Application.Users;

public class UserProfile
{
    public UserProfile(string email, string role)
    {
        Email = email;
        Role = role;
    }

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
