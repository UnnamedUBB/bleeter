using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.BleetsService.Services.Interfaces;

namespace Bleeter.BleetsService.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task AddIfNotExistsAsync(Guid id, string userName)
    {
        var exists = await _authorRepository.ExistsAsync(x => x.Id == id);
        if (exists) return;

        _authorRepository.Add(new AuthorModel
        {
            Id = id,
            UserName = userName,
        });
    }
}