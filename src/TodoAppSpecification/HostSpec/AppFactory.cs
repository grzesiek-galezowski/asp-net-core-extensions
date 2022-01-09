using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using TodoApp.Bootstrap;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace TodoAppSpecification.HostSpec;

public class AppFactory : WebApplicationFactory<ServiceLogicRoot>
{
  private readonly MemoryTarget _inMemoryLogs;
  private IHost _host;

  public AppFactory()
  {
    _inMemoryLogs = new MemoryTarget("memory");
  }

  protected override IHost CreateHost(IHostBuilder builder)
  {
    _host = base.CreateHost(builder);
    return _host;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services => 
      services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
      {
        options.TokenValidationParameters.ValidIssuer = TestTokens.Issuer;
        options.TokenValidationParameters.IssuerSigningKey = TestTokens.SecurityKey;
      }))
      .ConfigureLogging(loggingBuilder =>
      {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddNLog(_ =>
        {
          var loggingConfiguration = new LoggingConfiguration();
          ConfigForLogger.ConfigureAndAddLoggingTarget(
            _inMemoryLogs, 
            loggingConfiguration);
          return new LogFactory(loggingConfiguration);
        });
        loggingBuilder.AddNLogWeb();
      })
      .UseNLog();
  }

  public override async ValueTask DisposeAsync()
  {
    //bug this line belongs in the driver
    await TestContext.Out.WriteLineAsync(string.Join(Environment.NewLine, _inMemoryLogs.Logs));
    
    await _host.StopAsync();
    await base.DisposeAsync();
  }
}