using Bleeter.BleetsService.Services.Interfaces;
using FluentValidation;
using MediatR;

namespace Bleeter.BleetsService.Mediator.Commands;

public class AddBleetCommand : IRequest
{
    public string Content { get; set; }
}

public class AddBleetCommandValidator : AbstractValidator<AddBleetCommand>
{
    public AddBleetCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotNull().WithMessage("Zawartość bleeta musi zostać podana")
            .MinimumLength(10).WithMessage("Bleet musi składać się z przynajmniej 10 znaków");
    }
}

public class AddBleetCommandHandler : IRequestHandler<AddBleetCommand>
{
    private readonly IBleetService _bleetService;

    public AddBleetCommandHandler(IBleetService bleetService)
    {
        _bleetService = bleetService;
    }

    public Task Handle(AddBleetCommand request, CancellationToken cancellationToken)
    {
        return _bleetService.AddBleet(request.Content);
    }
}