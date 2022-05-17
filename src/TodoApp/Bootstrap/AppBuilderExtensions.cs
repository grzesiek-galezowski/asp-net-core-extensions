using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace TodoApp.Bootstrap;

public static class AppBuilderExtensions
{
  public static TokenValidationParameters GetTokenValidationParameters(this IServiceProvider serviceProvider)
  {
    return serviceProvider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>()
      .Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
  }

  public static void SetupNLog(this WebApplicationBuilder webApplicationBuilder)
  {
      webApplicationBuilder.Logging.ClearProviders();
      webApplicationBuilder.Logging.AddNLog(_ => Bootstrap.ServiceLogicRoot.CreateLogFactory());
      webApplicationBuilder.Logging.AddNLogWeb();
      webApplicationBuilder.Logging.SetMinimumLevel(LogLevel.Trace);
      webApplicationBuilder.Host.UseNLog();
  }

  public static ServiceLogicRoot ServiceLogicRoot(this Lazy<WebApplication> app)
  {
    return app.Value.Services.GetRequiredService<ServiceLogicRoot>();
  }
}