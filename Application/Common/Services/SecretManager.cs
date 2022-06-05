using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class SecretManager : ISecretManager
{
    private readonly SecretManagerSettings _settings;

    public SecretManager(IOptions<SecretManagerSettings> settings)
    {
        _settings = settings.Value;

        if (_settings.Key.Length < 15)
            throw new ArgumentException(nameof(SecretManagerSettings));
    }

    public string GenerateRandomString(int length)
    {
        if (length < 1) throw new ArgumentException();

        const string chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(
            Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public string HashPassword(string password, string secret)
    {
        if (secret.Length < 1 || password.Length < 1)
            throw new ArgumentException();

        byte[] bytes = Encoding.UTF8.GetBytes(password + secret);
        var sha = SHA256.Create();
        byte[] hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public string GenerateToken(IEnumerable<Claim> claims, DateTime expires)
    {
        if (claims is null || claims.Count() < 1 || expires <= Time.Now)
            throw new ArgumentException();

        var token = new JwtSecurityToken(
            _settings.Issuer, null, claims, Time.Now, expires,
            new SigningCredentials(
                MakeKey(_settings.Key!), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public IEnumerable<Claim>? ReadToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var parameters = TokenValidationParameters(
            MakeKey(_settings.Key!), _settings.Issuer!);
        parameters.ValidateLifetime = false;

        try
        {
            if (!(tokenHandler.ReadJwtToken(token)).Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase)) return null;

            return tokenHandler.ValidateToken(
                token, parameters, out var validatedToken).Claims.ToArray();
        }
        catch
        {
            return null;
        }
    }

    public static TokenValidationParameters TokenValidationParameters(
        SymmetricSecurityKey key, string issuer)
        => new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

    public static SymmetricSecurityKey MakeKey(string secret)
        => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
}

public class SecretManagerSettings
{
    public string Key { get; set; } = String.Empty;
    public string Issuer { get; set; } = String.Empty;
}
