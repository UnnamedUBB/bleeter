using Bleeter.Shared.Data.Interfaces;
using MediatR;

namespace Bleeter.Shared.Pipelines;

public class TransactionPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionPipeline(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        var result = await next();
        await _unitOfWork.CommitTransactionAsync();

        return result;
    }
}