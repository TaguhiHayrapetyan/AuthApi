using AuthService.Domain.Entities.Common;

namespace AuthService.Domain.Entities;

public class User : Entity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
}