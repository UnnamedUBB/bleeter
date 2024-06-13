using Bleeter.BleetsService.Services.Interfaces;
using MediatR;

namespace Bleeter.BleetsService.Mediator.Commands;

public class DeleteBleetCommand : IRequest
{
    public Guid BleetId { get; set; }
}

public class DeleteBleetCommandHandler : IRequestHandler<DeleteBleetCommand>
{
    private readonly IBleetService _bleetService;

    public DeleteBleetCommandHandler(IBleetService bleetService)
    {
        _bleetService = bleetService;
    }

    public Task Handle(DeleteBleetCommand request, CancellationToken cancellationToken)
    {
       return _bleetService.DeleteBleet(request.BleetId);
    }
}