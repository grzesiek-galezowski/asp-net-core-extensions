using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TodoApp.Bootstrap;

public static class ProgramExtension
{
  public static void AddTodoAppSpecificJwtAuthenticationConfig(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddAuthentication(option =>
    {
      option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Zenek",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Sekret"))
      };
    });
  }

}