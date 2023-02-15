namespace AuthService.Application.Models;

public sealed class Token
{
    public string Scheme { get; set; }
    public string Value { get; set; }
    public string Expires { get; set; }
}