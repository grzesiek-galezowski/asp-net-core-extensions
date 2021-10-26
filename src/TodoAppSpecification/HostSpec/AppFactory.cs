using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoApp.Bootstrap;

namespace TodoAppSpecification.HostSpec;

public class AppFactory : WebApplicationFactory<ServiceLogicRoot>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    base.ConfigureWebHost(builder);
  }
}