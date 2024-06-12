using System.Net;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.BleetsService.Dtos;
using Bleeter.Shared.Exceptions;

namespace Bleeter.BleetsService.Services;

public class CommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IBleetRepository _bleetRepository;
    
    public CommentService(ICommentRepository commentRepository, IBleetRepository bleetRepository)
    {
        _commentRepository = commentRepository;
        _bleetRepository = bleetRepository;
    }

    public async Task AddComment(AddCommentRequestDto dto)
    {
        var bleet = await _bleetRepository.ExistsAsync(x => x.Id == dto.BleetId);
        if (bleet)
        {
            throw new DomainException(HttpStatusCode.BadRequest, "Bleet nie istnieje");
        }
    }
}