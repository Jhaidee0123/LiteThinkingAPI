using LiteThinking.Infrastructure.Authentication;
using MediatR;

namespace LiteThinking.Application.Users.Commands;

public class RegisterAdminCommandHandler : AsyncRequestHandler<RegisterAdminCommand>
{
    private readonly IUserAuthenticationRepository _userRepository;

    public RegisterAdminCommandHandler(IUserAuthenticationRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        await _userRepository.RegisterAdminAsync(
            request.Email,
            request.Password);
    }
}
