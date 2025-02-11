using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Security.Enums;

namespace Application.Features.Auth.Commands.VerifyOtpAuthenticator;

public class VerifyOtpAuthenticatorCommand : IRequest, ISecuredRequest
{
    public Guid UserId { get; set; }
    public string ActivationCode { get; set; }

    public string[] Roles => [];

    public VerifyOtpAuthenticatorCommand()
    {
        ActivationCode = string.Empty;
    }

    public VerifyOtpAuthenticatorCommand(Guid userId, string activationCode)
    {
        UserId = userId;
        ActivationCode = activationCode;
    }

    public class VerifyOtpAuthenticatorCommandHandler(
        IOtpAuthenticatorRepository otpAuthenticatorRepository,
        AuthBusinessRules authBusinessRules,
        IUserService userService,
        IAuthenticatorService authenticatorService
        ) : IRequestHandler<VerifyOtpAuthenticatorCommand>
    {
        public async Task Handle(VerifyOtpAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            OtpAuthenticator? otpAuthenticator = await otpAuthenticatorRepository.GetAsync(
                predicate: e => e.UserId == request.UserId,
                cancellationToken: cancellationToken
            );
            await authBusinessRules.OtpAuthenticatorShouldBeExists(otpAuthenticator);

            User? user = await userService.GetAsync(
                predicate: u => u.Id == request.UserId,
                cancellationToken: cancellationToken
            );
            await authBusinessRules.UserShouldBeExistsWhenSelected(user);

            otpAuthenticator!.IsVerified = true;
            user!.AuthenticatorType = AuthenticatorType.Otp;

            await authenticatorService.VerifyAuthenticatorCode(user, request.ActivationCode);

            await otpAuthenticatorRepository.UpdateAsync(otpAuthenticator);
            await userService.UpdateAsync(user);
        }
    }
}
