using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Bleeter.Shared.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Bleeter.AccountsService.Mediator.Commands;

public class SignInCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {        
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Niepoprawny adres email");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Hasło jest wymagane.")
            .MinimumLength(8).WithMessage("Hasło musi składać się z przynajmniej 8 znaków")
            .Matches("[A-Z]").WithMessage("Hasło musi zawierać dużą literę")
            .Matches("[a-z]").WithMessage("Hasło musi zawierać małą literę")
            .Matches("[0-9]").WithMessage("Hasło musi zawierać cyfrę")
            .Matches("[@$!%*#?&]").WithMessage("Hasło musi zawierać znak specjalny");
    }
}

public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    
    public SignInCommandHandler(UserManager<IdentityUser<Guid>> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) 
            throw new DomainException(HttpStatusCode.NotFound, "Konto o podanym adresie email nie istnieje");

        var password = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (password == PasswordVerificationResult.Failed)
            throw new DomainException(HttpStatusCode.Unauthorized, "Podano niepoprawne haśło");
        
        Console.WriteLine(user.Id.ToString());
        
        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ktowcuroicmpyseqskuitzgyvgwmvxkx"));
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, (user.UserName ?? user.Email) ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Aud, "http://localhost:5401"),
                new Claim(JwtRegisteredClaimNames.Aud, "http://localhost:5402"),
            }),
            Expires = DateTime.Now.AddMinutes(5),
            SigningCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256),
            Issuer = "http://localhost:5401",
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }
}