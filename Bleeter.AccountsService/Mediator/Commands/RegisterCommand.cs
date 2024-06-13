using System.Text;
using System.Text.Encodings.Web;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Bleeter.AccountService.Mediator.Commands;

public class RegisterCommand : IRequest
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(UserManager<IdentityUser<Guid>> userManager)
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Nazwa użytkwonika jest wymagana")
            .MinimumLength(3).WithMessage("Nazwa użytkwonika musi zawierać przynamniej 3 znaki")
            .MustAsync(async (x, _) =>
            { 
                var user = await userManager.FindByNameAsync(x);
                return user == null;
            }).WithMessage("Użytkwonik o takiej nazwie użytkownika już istnieje"); ;
        
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Niepoprawny adres email")
            .MustAsync(async (x, _) =>
            { 
                var user = await userManager.FindByEmailAsync(x);
                return user == null;
            }).WithMessage("Użytkwonik o takim adresie email już istnieje");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Hasło jest wymagane.")
            .MinimumLength(8).WithMessage("Hasło musi składać się z przynajmniej 8 znaków")
            .Matches("[A-Z]").WithMessage("Hasło musi zawierać dużą literę")
            .Matches("[a-z]").WithMessage("Hasło musi zawierać małą literę")
            .Matches("[0-9]").WithMessage("Hasło musi zawierać cyfrę")
            .Matches("[@$!%*#?&]").WithMessage("Hasło musi zawierać znak specjalny");
    }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly IUserStore<IdentityUser<Guid>> _userStore;
    private readonly IEmailSender<IdentityUser<Guid>> _emailSender;

    public RegisterCommandHandler(UserManager<IdentityUser<Guid>> userManager, IUserStore<IdentityUser<Guid>> userStore, IEmailSender<IdentityUser<Guid>> emailSender, LinkGenerator linkGenerator)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailSender = emailSender;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailStore = (IUserEmailStore<IdentityUser<Guid>>) _userStore;
        
        var user = new IdentityUser<Guid>();
        await _userStore.SetUserNameAsync(user, request.UserName, CancellationToken.None);
        await emailStore.SetEmailAsync(user, request.Email, CancellationToken.None);
        await _userManager.CreateAsync(user, request.Password);
        
        await _emailSender.SendConfirmationLinkAsync(user, request.Email, HtmlEncoder.Default.Encode("Wysłalno email"));
    }
}