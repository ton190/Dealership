using System.Security.Claims;

namespace Application.Interfaces;

public interface ISecretManager
{
    string? GenerateRandomString(int length);

    string GenerateRefreshToken();

    string? HashPassword(string password, string secret);

    string? GenerateToken(IEnumerable<Claim> claims, DateTime expiration);

    IEnumerable<Claim>? ReadToken(string token);
}
