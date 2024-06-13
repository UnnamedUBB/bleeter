using Bleeter.BleetsService.Dtos;

namespace Bleeter.BleetsService.Services.Interfaces;

public interface IBleetService
{
    Task AddBleet(string content);
    Task DeleteBleet(Guid bleetId);
}