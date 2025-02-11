using Application.Features.Auth.Constants;
using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using AutoMapper;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Auth.Constants.AuthOperationClaims;

namespace Application.Features.Auth.Commands.RevokeToken;

public class RevokeTokenCommand : IRequest<RevokedTokenResponse>, ISecuredRequest
{
    public string Token { get; set; }
    public string IpAddress { get; set; }

    public string[] Roles => [Admin, AuthOperationClaims.RevokeToken];

    public RevokeTokenCommand()
    {
        Token = string.Empty;
        IpAddress = string.Empty;
    }

    public RevokeTokenCommand(string token, string ipAddress)
    {
        Token = token;
        IpAddress = ipAddress;
    }

    public class RevokeTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IMapper mapper) : IRequestHandler<RevokeTokenCommand, RevokedTokenResponse>
    {
        public async Task<RevokedTokenResponse> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.RefreshToken? refreshToken = await authService.GetRefreshTokenByToken(request.Token);
            await authBusinessRules.RefreshTokenShouldBeExists(refreshToken);
            await authBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

            await authService.RevokeRefreshToken(token: refreshToken!, request.IpAddress, reason: "Revoked without replacement");

            RevokedTokenResponse revokedTokenResponse = mapper.Map<RevokedTokenResponse>(refreshToken);
            return revokedTokenResponse;
        }
    }
}
