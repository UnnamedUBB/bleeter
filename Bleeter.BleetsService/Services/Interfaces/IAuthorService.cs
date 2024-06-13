namespace Bleeter.BleetsService.Services.Interfaces;

public interface IAuthorService
{
    Task AddIfNotExistsAsync(Guid id, string userName);
}