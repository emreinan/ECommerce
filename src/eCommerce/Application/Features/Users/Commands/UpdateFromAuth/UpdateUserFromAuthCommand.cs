using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommand : IRequest<UpdatedUserFromAuthResponse>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }

    public UpdateUserFromAuthCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Password = string.Empty;
    }

    public UpdateUserFromAuthCommand(Guid id, string firstName, string lastName, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }

    public class UpdateUserFromAuthCommandHandler(
        IUserRepository userRepository,
        IMapper mapper,
        UserBusinessRules userBusinessRules,
        IAuthService authService
        ) : IRequestHandler<UpdateUserFromAuthCommand, UpdatedUserFromAuthResponse>
    {
        public async Task<UpdatedUserFromAuthResponse> Handle(
            UpdateUserFromAuthCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await userRepository.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await userBusinessRules.UserPasswordShouldBeMatched(user: user!, request.Password);
            await userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);

            user = mapper.Map(request, user);
            if (request.NewPassword != null && !string.IsNullOrWhiteSpace(request.NewPassword))
            {
                HashingHelper.CreatePasswordHash(
                    request.Password,
                    passwordHash: out byte[] passwordHash,
                    passwordSalt: out byte[] passwordSalt
                );
                user!.PasswordHash = passwordHash;
                user!.PasswordSalt = passwordSalt;
            }

            User updatedUser = await userRepository.UpdateAsync(user!);

            UpdatedUserFromAuthResponse response = mapper.Map<UpdatedUserFromAuthResponse>(updatedUser);
            response.AccessToken = await authService.CreateAccessToken(user!);
            return response;
        }
    }
}
