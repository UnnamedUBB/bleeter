using System.Net;
using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.BleetsService.Services.Interfaces;
using Bleeter.Shared.Exceptions;
using Bleeter.Shared.Services.Interfaces;

namespace Bleeter.BleetsService.Services;

public class BleetService : IBleetService
{
    private readonly IBleetRepository _bleetRepository;
    private readonly IAuthorService _authorService;
    private readonly IUserClaimService _userClaimService;

    public BleetService(IBleetRepository bleetRepository, IAuthorService authorService, IUserClaimService userClaimService)
    {
        _bleetRepository = bleetRepository;
        _authorService = authorService;
        _userClaimService = userClaimService;
    }

    public async Task AddBleet(string content)
    {
        await _authorService.AddIfNotExistsAsync(_userClaimService.GetUserId(), _userClaimService.GetUserName());
        _bleetRepository.Add(new BleetModel()
        {
            AuthorId = _userClaimService.GetUserId(),
            Content = content
        });
    }

    public async Task DeleteBleet(Guid bleetId)
    {
        var bleet = await _bleetRepository.GetAsync(x => x.Id == bleetId);
        if (bleet == null)
        {
            throw new DomainException(HttpStatusCode.NotFound, "Bleet, który próbujesz usunąć nie istnieje");
        }
        
        _bleetRepository.Delete(bleet);
    }
}