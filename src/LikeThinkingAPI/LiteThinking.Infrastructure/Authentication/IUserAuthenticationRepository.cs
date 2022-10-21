namespace LiteThinking.Infrastructure.Authentication;

public interface IUserAuthenticationRepository
{
    /// <summary>
    /// Saves a new user and attaches the admin role to it
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task RegisterAdminAsync(string email, string password);

    /// <summary>
    /// Saves a new user and attaches the external role to it
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task RegisterExternalAsync(string email, string password);

    /// <summary>
    /// Attempts to log in given an username and a password
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>A tuple with the JWT and the date up until the token is valid</returns>
    Task<(string, DateTime)> LoginAsync(string username, string password);
}
