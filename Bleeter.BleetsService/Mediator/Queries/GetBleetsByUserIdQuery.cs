using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.Shared.Data;
using Bleeter.Shared.Services.Interfaces;
using MediatR;

namespace Bleeter.BleetsService.Mediator.Queries;

public class GetBleetsByUserIdQuery : IRequest<PageableList<BleetModel>>
{
    public int PageSize { get; set; }
    public int Page { get; set; }
}

public class GetBleetsByUserIdQueryHandler : IRequestHandler<GetBleetsByUserIdQuery, PageableList<BleetModel>>
{
    private readonly IBleetRepository _bleetRepository;
    private readonly IUserClaimService _userClaimService;

    public GetBleetsByUserIdQueryHandler(IBleetRepository bleetRepository, IUserClaimService userClaimService)
    {
        _bleetRepository = bleetRepository;
        _userClaimService = userClaimService;
    }

    public Task<PageableList<BleetModel>> Handle(GetBleetsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return _bleetRepository.GetAllWithPaginationAsync(x => x.AuthorId == _userClaimService.GetUserId());
    }

}