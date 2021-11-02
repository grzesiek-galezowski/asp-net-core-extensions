using System;
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
  public static SigningCredentials SigningCredentials { get; }

  private static readonly JwtSecurityTokenHandler TokenHandler = new();
  private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
  private static readonly byte[] Key = new byte[32];

  static TestTokens()
  {
    Rng.GetBytes(Key);
    SecurityKey = new SymmetricSecurityKey(Key) { KeyId = Guid.NewGuid().ToString() };
    SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
  }

  public static string GenerateToken(params Claim[] claims)
  {
    return TokenHandler.WriteToken(
      new JwtSecurityToken(
        Issuer,
        null,
        claims,
        null,
        DateTime.UtcNow.AddMinutes(20),
        SigningCredentials));
  }
}