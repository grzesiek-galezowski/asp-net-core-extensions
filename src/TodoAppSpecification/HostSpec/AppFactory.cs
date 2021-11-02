using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Bootstrap;

namespace TodoAppSpecification.HostSpec;

public class AppFactory : WebApplicationFactory<ServiceLogicRoot>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services => 
      services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
      {
        options.TokenValidationParameters.ValidIssuer = TestTokens.Issuer;
        options.TokenValidationParameters.IssuerSigningKey = TestTokens.SecurityKey;
      }));
  }
}