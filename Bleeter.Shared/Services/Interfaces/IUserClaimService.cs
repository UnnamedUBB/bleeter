namespace Bleeter.Shared.Services.Interfaces;

public interface IUserClaimService
{
    Guid GetUserId();
    string GetUserName();
}