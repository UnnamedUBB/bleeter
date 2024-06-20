using Bleeter.Shared.Exceptions;
using Bleeter.Shared.Pipelines;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace Bleeter.Tests.Unit;

public class ValidationPipelineTests
{
    private readonly Mock<IValidator<TestRequest>> _validatorMock;
    private readonly Mock<RequestHandlerDelegate<TestResponse>> _nextMock;
    private readonly ValidationPipeline<TestRequest, TestResponse> _pipeline;

    public ValidationPipelineTests()
    {
        _validatorMock = new Mock<IValidator<TestRequest>>();
        _nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        _pipeline = new ValidationPipeline<TestRequest, TestResponse>(new[] { _validatorMock.Object });
    }

    [Fact]
    public async Task Handle_NoValidators_ShouldCallNext()
    {
        // Arrange
        var pipeline = new ValidationPipeline<TestRequest, TestResponse>(Enumerable.Empty<IValidator<TestRequest>>());
        _nextMock.Setup(next => next()).ReturnsAsync(new TestResponse());

        // Act
        var response = await pipeline.Handle(new TestRequest(), _nextMock.Object, CancellationToken.None);

        // Assert
        _nextMock.Verify(next => next(), Times.Once);
        Assert.IsType<TestResponse>(response);
    }

    [Fact]
    public async Task Handle_ValidatorsReturnNoErrors_ShouldCallNext()
    {
        // Arrange
        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);
        _nextMock.Setup(next => next()).ReturnsAsync(new TestResponse());

        // Act
        var response = await _pipeline.Handle(new TestRequest(), _nextMock.Object, CancellationToken.None);

        // Assert
        _nextMock.Verify(next => next(), Times.Once);
        Assert.IsType<TestResponse>(response);
    }

    [Fact]
    public async Task Handle_ValidatorsReturnErrors_ShouldThrowValidatorException()
    {
        // Arrange
        var validationFailures = new List<ValidationFailure>
        {
            new ("Field1", "Error1"),
            new ("Field1", "Error2"),
            new ("Field2", "Error3")
        };
        var validationResult = new ValidationResult(validationFailures);
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidatorException>(() => _pipeline.Handle(new TestRequest(), _nextMock.Object, CancellationToken.None));

        Assert.NotNull(exception.Errors);
        Assert.True(exception.Errors.ContainsKey("Field1"));
        Assert.Contains("Error1", exception.Errors["Field1"]);
        Assert.Contains("Error2", exception.Errors["Field1"]);
        Assert.True(exception.Errors.ContainsKey("Field2"));
        Assert.Contains("Error3", exception.Errors["Field2"]);
        _nextMock.Verify(next => next(), Times.Never);
    }
}

public class TestRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class TestResponse
{
    public bool Success { get; set; }
}