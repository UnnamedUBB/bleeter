using System.Net;
using System.Text;
using Bleeter.AccountsService.Data;
using Bleeter.AccountsService.Mediator.Commands;
using Bleeter.Tests.Shared;
using Newtonsoft.Json;

namespace Bleeter.Tests.Functional.AccountService;

public class LoginTests : BaseFunctionalTest<AccountContext, Bleeter.AccountsService.Program>
{
    public LoginTests(FunctionalTestWebAppFactory<AccountContext, Bleeter.AccountsService.Program> factory) :
        base(factory)
    {
    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenValidCommand()
    {
        // Arrange
        var registerCommand = new RegisterCommand
        {
            Email = "test@example.com",
            UserName = "testuser1",
            Password = "Test@1234"
        };

        await Mediator.Send(registerCommand);

        var loginCommand = new SignInCommand
        {
            Email = registerCommand.Email,
            Password = registerCommand.Password
        };
        var content = new StringContent(JsonConvert.SerializeObject(loginCommand), Encoding.UTF8, "application/json");

        // Act
        var response = await HttpClient.PostAsync("/Account", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var token = await response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenInvalidPassword()
    {
        // Arrange
        var registerCommand = new RegisterCommand
        {
            Email = "test1@example.com",
            UserName = "testuser2",
            Password = "Test@1234"
        };

        await Mediator.Send(registerCommand);
        
        var loginCommand = new SignInCommand
        {
            Email = "test1@example.com",
            Password = "Qwerty123$"
        };
        var content = new StringContent(JsonConvert.SerializeObject(loginCommand), Encoding.UTF8, "application/json");

        // Act
        var response = await HttpClient.PostAsync("/Account", content);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnNotFound_WhenEmailDoesNotExist()
    {
        // Arrange
        var loginCommand = new SignInCommand
        {
            Email = "nonexistent@example.com",
            Password = "Test@1234"
        };
        var content = new StringContent(JsonConvert.SerializeObject(loginCommand), Encoding.UTF8, "application/json");

        // Act
        var response = await HttpClient.PostAsync("/Account", content);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}