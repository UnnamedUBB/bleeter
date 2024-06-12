using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.BleetsService.Dtos;
using Mapster;

namespace Bleeter.BleetsService.Services;

public class BleetService
{
    private readonly IBleetRepository _bleetRepository;
    private readonly IAuthorRepository _authorRepository;

    public BleetService(IBleetRepository bleetRepository, ICommentRepository commentRepository, IAuthorRepository authorRepository)
    {
        _bleetRepository = bleetRepository;
        _authorRepository = authorRepository;
    }

    public async Task AddBleet(AddCommentRequestDto dto)
    {
        // var author = await _authorRepository.ExistsAsync(x => x.Id != )
        var model = dto.Adapt<BleetModel>();
        _bleetRepository.Add(model);
    }

    public async Task EditBleet()
    {
        
    }

    public async Task DeleteBleet()
    {
        
    }
}