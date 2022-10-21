using LiteThinking.Infrastructure.Authentication;
using MediatR;

namespace LiteThinking.Application.Authentication.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
{
    private readonly IUserAuthenticationRepository _userRepository;

    public LoginQueryHandler(IUserAuthenticationRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var (token, validTo) = await _userRepository.LoginAsync(request.Email, request.Password);

        return new LoginResponse
        {
            Token = token,
            ValidTo = validTo
        };
    }
}
