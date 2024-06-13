using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.Shared.Data;
using Bleeter.Shared.Services.Interfaces;
using MediatR;

namespace Bleeter.BleetsService.Mediator.Queries;

public class GetBleetsQuery : IRequest<PageableList<BleetModel>>
{
    public int PageSize { get; set; }
    public int Page { get; set; }
}

public class GetBleetsQueryHandler : IRequestHandler<GetBleetsQuery, PageableList<BleetModel>>
{
    private readonly IBleetRepository _bleetRepository;
    private readonly IUserClaimService _userClaimService;

    public GetBleetsQueryHandler(IBleetRepository bleetRepository, IUserClaimService userClaimService)
    {
        _bleetRepository = bleetRepository;
        _userClaimService = userClaimService;
    }

    public Task<PageableList<BleetModel>> Handle(GetBleetsQuery request, CancellationToken cancellationToken)
    {
        return _bleetRepository.GetAllWithPaginationAsync(x => x.AuthorId != _userClaimService.GetUserId());
    }
}