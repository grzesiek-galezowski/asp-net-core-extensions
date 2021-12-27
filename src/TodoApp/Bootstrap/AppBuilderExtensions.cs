using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TodoApp.Bootstrap;

public static class AppBuilderExtensions
{
  public static TokenValidationParameters GetTokenValidationParameters(this IServiceProvider serviceProvider)
  {
    return serviceProvider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>().Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
  }
}