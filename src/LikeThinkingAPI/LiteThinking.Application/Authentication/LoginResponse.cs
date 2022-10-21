namespace LiteThinking.Application.Authentication;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;    

    public DateTime ValidTo { get; set; }
}
