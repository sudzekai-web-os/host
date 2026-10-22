
using Presentation.src.Extensions.ServiceCollection;
using Sudzekai.WebOs.Hosting;

namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddPluginInfrastructure();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            builder.Services.ConfigureRateLimiting();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
                app.MapOpenApi();

            app.UseAuthentication();
            app.UseAuthorization();

            await app.LoadPluginsAsync();
            app.AddHandlers();

            app.Run();
        }
    }
}
