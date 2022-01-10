using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace TodoAppSpecification;

/// <summary>
/// from https://stebet.net/mocking-jwt-tokens-in-asp-net-core-integration-tests/
/// </summary>
public static class TestTokens
{
  public static string Issuer { get; } = Guid.NewGuid().ToString();
  public static SecurityKey SecurityKey { get; }
  private static SigningCredentials SigningCredentials { get; }

  private static readonly JwtSecurityTokenHandler TokenHandler = new();
  private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
  private static readonly byte[] Key = new byte[32];

  static TestTokens()
  {
    Rng.GetBytes(Key);
    SecurityKey = CreateSymmetricSecurityKey(Key);
    SigningCredentials = CreateSigningCredentials(SecurityKey);
  }

  public static string GenerateToken(params Claim[] claims)
  {
    return GenerateToken(claims, Issuer, SigningCredentials, DateTime.UtcNow.AddMinutes(20));
  }

  public static string GenerateTokenWithBadKey(params Claim[] claims)
  {
    var signingCredentials = CreateSigningCredentials(
      CreateSymmetricSecurityKey(
        Enumerable.Repeat<byte>(1, 32).ToArray()));
    return GenerateToken(claims, Issuer, signingCredentials, DateTime.UtcNow.AddMinutes(20));
  }

  public static string GenerateTokenFromBadIssuer(params Claim[] claims)
  {
    return GenerateToken(claims, Issuer + Any.Char(), SigningCredentials, DateTime.UtcNow.AddMinutes(20));
  }

  public static string GenerateExpiredToken(params Claim[] claims)
  {
    return GenerateToken(claims, Issuer, SigningCredentials, DateTime.UtcNow.AddMinutes(-20));
  }

  private static string GenerateToken(Claim[] claims, string issuer, SigningCredentials signingCredentials, DateTime expiryTime)
  {
    var generatedToken = TokenHandler.WriteToken(
      new JwtSecurityToken(
        issuer,
        null,
        claims,
        null,
        expiryTime,
        signingCredentials));

    return generatedToken;
  }

  private static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
  {
    return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
  }

  private static SymmetricSecurityKey CreateSymmetricSecurityKey(byte[] key)
  {
    return new SymmetricSecurityKey(key) { KeyId = Guid.NewGuid().ToString() };
  }
}