using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class AppUser :IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } =string.Empty;
    public string Roles { get; set; } = "user";
    public List<Portifolio> Portifolios { get; set; } = new List<Portifolio>();
}
