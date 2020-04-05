using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TodoApp.Bootstrap
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddSingleton(x => new ServiceLogicRoot());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseAuthorization();


      app.UseEndpoints(endpoints =>
        {
          endpoints.MapPost("/todo",
            async context =>
            {
              await context.RequestServices.GetRequiredService<ServiceLogicRoot>().AddTodoAction()
                .ExecuteAsync(context.Request, context.Response, context.RequestAborted);
            });

          endpoints.MapPost("/todo/{id1}/link/{id2}",
            async context =>
            {
              await context.RequestServices.GetRequiredService<ServiceLogicRoot>().LinkTodoAction()
                .ExecuteAsync(context.Request, context.Response, context.RequestAborted);
            });
        });
    }
  }
}
