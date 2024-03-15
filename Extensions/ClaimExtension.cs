using System.Security.Claims;
using api.Data.Migrations;

namespace api.Extensions;

public static class ClaimExtension
{
    public static string? GetUserId(this ClaimsPrincipal claims)
    {
        return claims.Claims.SingleOrDefault(claim => claim.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
    }
}
