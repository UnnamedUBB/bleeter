using System.Net;

namespace Bleeter.Shared.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public HttpStatusCode StatusCode;
    public string Description;

    public DomainException(HttpStatusCode statusCode, string description)
    {
        StatusCode = statusCode;
        Description = description;
    }
}