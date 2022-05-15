using System.Net;
using FluentAssertions.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Targets;
using NLog.Web;
using Polly;
using TodoApp.Bootstrap;
using TodoApp.Support;

namespace TodoAppSpecification.HostSpecification.Automation;

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
      {
        services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
        {
          options.TokenValidationParameters.ValidIssuer = TestTokens.Issuer;
          options.TokenValidationParameters.IssuerSigningKey = TestTokens.SecurityKey;
        });
      })
      .ConfigureLogging(loggingBuilder =>
      {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddNLog(_ => LoggingAdapter.CreateLogFactory(_inMemoryLogs));
        loggingBuilder.AddNLogWeb();
      })
      .UseNLog();
  }

  public override async ValueTask DisposeAsync()
  {
    await _host.StopAsync();
    await base.DisposeAsync();
  }

  public async Task PrintLogs()
  {
    await TestContext.Out.WriteLineAsync(string.Join(Environment.NewLine, _inMemoryLogs.Logs));
  }

  public FlurlClient FlurlClient()
  {
    return new FlurlClient(CreateClient());
  }

  public Task<IFlurlResponse> ReadinessCheck()
  {
    return Policy.Handle<FlurlHttpException>(exception => exception.StatusCode != (int)HttpStatusCode.NotFound)
      .WaitAndRetryAsync(20, _ => 1.Seconds())
      .ExecuteAsync(async () => await FlurlClient().Request("health").GetAsync());
  }
}