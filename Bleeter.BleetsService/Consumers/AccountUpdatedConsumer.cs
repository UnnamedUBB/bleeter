using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.Shared.Data.Interfaces;
using Bleeter.Shared.Messages;
using MassTransit;

namespace Bleeter.BleetsService.Consumers;

public class AccountUpdatedConsumer : IConsumer<AccountUpdatedMessage>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountUpdatedConsumer(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<AccountUpdatedMessage> context)
    {
        await _unitOfWork.BeginTransactionAsync();
        
        var author = await _authorRepository.GetAsync(x => x.Id == context.Message.UserId);
        if (author == null)
        {
            return;
        }
        
        _authorRepository.Update(author);
        author.UserName = context.Message.UserName;

        await _unitOfWork.CommitTransactionAsync();
    }
}