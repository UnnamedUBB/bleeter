namespace Bleeter.AccountsService.Utils;

public class JwtSecurityTokenSettings
{
    public string Issuer { get; set; }
    public List<string> Audiences { get; set; }
    public string SignInKey { get; set; }
    public int Expires { get; set; }
}