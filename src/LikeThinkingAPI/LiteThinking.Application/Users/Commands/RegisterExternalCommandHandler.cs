using LiteThinking.Infrastructure.Authentication;
using MediatR;

namespace LiteThinking.Application.Users.Commands;

public class RegisterExternalCommandHandler : AsyncRequestHandler<RegisterExternalCommand>
{
    private readonly IUserAuthenticationRepository _userRepository;

    public RegisterExternalCommandHandler(IUserAuthenticationRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task Handle(RegisterExternalCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        await _userRepository.RegisterExternalAsync(
            request.Email,
            request.Password);
    }
}
