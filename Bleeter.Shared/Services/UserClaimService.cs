using System.Net;
using System.Security.Claims;
using Bleeter.Shared.Exceptions;
using Bleeter.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Bleeter.Shared.Services;

public class UserClaimService : IUserClaimService
{
    private readonly IHttpContextAccessor _accessor;
    private IEnumerable<Claim>? Claims => _accessor.HttpContext.User.Claims;

    public UserClaimService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;

        if (_accessor.HttpContext?.User is null)
            throw new DomainException(HttpStatusCode.Unauthorized, "");
    }

    public Guid GetUserId() =>
        Guid.Parse(Claims.Single(x => x.Type == JwtRegisteredClaimNames.Sid).Value);
    
    public string GetUserName() =>
        Claims.Single(x => x.Type == JwtRegisteredClaimNames.Name).Value;
}