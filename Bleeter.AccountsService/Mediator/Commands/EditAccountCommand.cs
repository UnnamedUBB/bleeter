using System.Data;
using System.Net;
using Bleeter.Shared.Exceptions;
using Bleeter.Shared.Messages;
using Bleeter.Shared.Services.Interfaces;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.Mediator.Commands;

public class EditAccountCommand : IRequest
{
    public string UserName { get; set; }
}

public class EditAccountCommandValidator : AbstractValidator<EditAccountCommand>
{
    public EditAccountCommandValidator(UserManager<IdentityUser<Guid>> userManager)
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Nazwa użytkwonika jest wymagana")
            .MinimumLength(3).WithMessage("Nazwa użytkwonika musi zawierać przynamniej 3 znaki");
    }
}

public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand>
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly IUserClaimService _userClaimService;
    private readonly IUserStore<IdentityUser<Guid>> _userStore;
    private readonly IPublishEndpoint _publishEndpoint;

    public EditAccountCommandHandler(UserManager<IdentityUser<Guid>> userManager, IUserClaimService userClaimService, IUserStore<IdentityUser<Guid>> userStore, IPublishEndpoint publishEndpoint)
    {
        _userManager = userManager;
        _userClaimService = userClaimService;
        _userStore = userStore;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userStore.FindByIdAsync(_userClaimService.GetUserId().ToString(), cancellationToken);
        if (user == null)
        {
            throw new DomainException(HttpStatusCode.Unauthorized, "");
        }

        var newNormalizedUserName = _userManager.NormalizeName(request.UserName);
        if (user.NormalizedUserName == newNormalizedUserName) return;
        
        var userWithTheSameName = await _userManager.FindByNameAsync(request.UserName);
        if (userWithTheSameName != null)
        {
            throw new DomainException(HttpStatusCode.BadRequest, "Użytkownik o takiej nazwie już istnieje!");
        }

        await _userStore.SetUserNameAsync(user, request.UserName, cancellationToken);
        await _userManager.UpdateAsync(user);
        await _publishEndpoint.Publish(new AccountUpdatedMessage()
        {
            UserId = user.Id,
            UserName = request.UserName
        }, cancellationToken);
    }
}