using System.Net;
using System.Text;
using System.Text.Json;
using Bleeter.AccountsService.Data;
using Bleeter.AccountsService.Mediator.Commands;
using Bleeter.Tests.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bleeter.Tests.Functional.AccountService;

public class RegisterTests : BaseFunctionalTest<AccountContext, AccountsService.Program>
{
    public RegisterTests(FunctionalTestWebAppFactory<AccountContext, AccountsService.Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenValidCommand()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Email = "test@example.com",
            UserName = "testuser",
            Password = "Test@1234"
        };
        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        // Act
        var response = await HttpClient.PostAsync("/Account/create", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Additional checks for side effects like user creation can be done here
        var context = Scope.ServiceProvider.GetRequiredService<AccountContext>();
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == command.Email);
        Assert.NotNull(user);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenInvalidCommand()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Email = "invalid-email",
            UserName = "us",
            Password = "short"
        };
        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        // Act
        var response = await HttpClient.PostAsync("/Account/create", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}