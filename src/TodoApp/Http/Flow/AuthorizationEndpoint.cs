using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Sprache;

namespace TodoApp.Http.Flow;

public class AuthorizationEndpoint : IAsyncEndpoint
{
  private readonly TokenValidationParameters _tokenValidationParameters;
  private readonly IAsyncEndpoint _next;

  public AuthorizationEndpoint(TokenValidationParameters tokenValidationParameters, IAsyncEndpoint next)
  {
    _tokenValidationParameters = tokenValidationParameters;
    _next = next;
  }

  public async Task HandleAsync(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
  {
    var invokeNext = false;
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      string authorizationContent = request.Headers["Authorization"];
      Parser<string> parser = Parse.String("Bearer ").Then(_ =>
        from rest in Parse.AnyChar.Many().Text().Token()
        select rest);
      var token = parser.Parse(authorizationContent);

      if (tokenHandler.CanReadToken(token))
      {
        var claimsPrincipal = tokenHandler.ValidateToken(
          token, 
          new TokenValidationParameters()
          {
            ValidIssuer = _tokenValidationParameters.ValidIssuer,
            IssuerSigningKey = _tokenValidationParameters.IssuerSigningKey,
            RequireAudience = false,
            ValidateActor = false,
            ValidateAudience = false,
            ValidateTokenReplay = false,
            //AuthenticationType = 
          }, out var securityToken);

        if (securityToken != null)
        {
          invokeNext = true;
        }
      }
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      //bug log the exception!
    }

    if (invokeNext)
    {
      await _next.HandleAsync(request, response, cancellationToken);
    }
    else
    {
      await Results.Unauthorized().ExecuteAsync(request.HttpContext);
    }
  }
}