using System.Net;
using Bleeter.Shared.Exceptions;
using Bleeter.Shared.Middlewares;
using Microsoft.AspNetCore.Http;
using Moq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Bleeter.Tests.Unit;

public class ExceptionMiddlewareTests
{
    private readonly ExceptionMiddleware _middleware;
    private readonly Mock<RequestDelegate> _nextMock;
    private readonly DefaultHttpContext _httpContext;

    public ExceptionMiddlewareTests()
    {
        _middleware = new ExceptionMiddleware();
        _nextMock = new Mock<RequestDelegate>();
        _httpContext = new DefaultHttpContext();
    }

    [Fact]
    public async Task InvokeAsync_WhenValidatorExceptionIsThrown_ShouldReturnBadRequestWithErrors()
    {
        var errors = new Dictionary<string, List<string>>
        {
            { "Field1", new List<string> { "Error1", "Error2" } },
            { "Field2", new List<string> { "Error3" } }
        };
        var validatorException = new ValidatorException(errors);
        _nextMock.Setup(next => next(It.IsAny<HttpContext>())).ThrowsAsync(validatorException);

        _httpContext.Response.Body = new MemoryStream();

        // Act
        await _middleware.InvokeAsync(_httpContext, _nextMock.Object);

        await _httpContext.Response.Body.FlushAsync();
        _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

        // Assert
        Assert.Equal(StatusCodes.Status400BadRequest, _httpContext.Response.StatusCode);

        var response = await new StreamReader(_httpContext.Response.Body).ReadToEndAsync();
        var responseObject = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(response);

        Assert.NotNull(responseObject);
        Assert.True(responseObject.ContainsKey("Field1"));
        Assert.Contains("Error1", responseObject["Field1"]);
        Assert.Contains("Error2", responseObject["Field1"]);
        Assert.True(responseObject.ContainsKey("Field2"));
        Assert.Contains("Error3", responseObject["Field2"]);
    }

    [Fact]
    public async Task InvokeAsync_WhenDomainExceptionIsThrown_ShouldReturnCustomStatusCodeWithDescription()
    {
        // Arrange
        var domainException = new DomainException(HttpStatusCode.Forbidden,"Domain error occurred");
        _nextMock.Setup(next => next(It.IsAny<HttpContext>())).ThrowsAsync(domainException);

        _httpContext.Response.Body = new MemoryStream();
        
        // Act
        await _middleware.InvokeAsync(_httpContext, _nextMock.Object);
        
        await _httpContext.Response.Body.FlushAsync();
        _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

        // Assert
        Assert.Equal(StatusCodes.Status403Forbidden, _httpContext.Response.StatusCode);

        var response = await new StreamReader(_httpContext.Response.Body).ReadToEndAsync();
        Assert.NotNull(response);
        Assert.Contains("Domain error occurred", response);
    }

    [Fact]
    public async Task InvokeAsync_WhenNoExceptionIsThrown_ShouldCallNextMiddleware()
    {
        // Arrange
        _nextMock.Setup(next => next(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

        // Act
        await _middleware.InvokeAsync(_httpContext, _nextMock.Object);

        // Assert
        _nextMock.Verify(next => next(It.IsAny<HttpContext>()), Times.Once);
    }
}