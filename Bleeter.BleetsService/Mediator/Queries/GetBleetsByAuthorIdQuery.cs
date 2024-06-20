using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.BleetsService.Dtos;
using Bleeter.Shared.Data;
using Bleeter.Shared.Services.Interfaces;
using FluentValidation;
using MediatR;

namespace Bleeter.BleetsService.Mediator.Queries;

public class GetBleetsByAuthorIdQuery : IRequest<PageableList<GetBleetDto>>
{
    public int PageSize { get; set; } = 20;
    public int Page { get; set; } = 1;
}

public class GetBleetsByAuthorIdQueryValidator : AbstractValidator<GetBleetsByAuthorIdQuery>
{
    public GetBleetsByAuthorIdQueryValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize musi być większe niż 0")
            .LessThanOrEqualTo(50).WithMessage("PageSize musi być równe lub mniejsze od 50");

        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page musi być większe niż 0");
    }
}

public class GetBleetsByAuthorIdQueryHandler : IRequestHandler<GetBleetsByAuthorIdQuery, PageableList<GetBleetDto>>
{
    private readonly IBleetRepository _bleetRepository;
    private readonly IUserClaimService _userClaimService;

    public GetBleetsByAuthorIdQueryHandler(IBleetRepository bleetRepository, IUserClaimService userClaimService)
    {
        _bleetRepository = bleetRepository;
        _userClaimService = userClaimService;
    }

    public Task<PageableList<GetBleetDto>> Handle(GetBleetsByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        return _bleetRepository.GetAllWithPaginationAsync<GetBleetDto>(
            x => x.AuthorId == _userClaimService.GetUserId(),
            null,
            request.Page,
            request.PageSize
        );
    }
}