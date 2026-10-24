using Presentation.src.Extensions.LoggingBuilder;
using Presentation.src.Extensions.ServiceCollection;
using Presentation.src.Extensions.WebApplication;
using Sudzekai.WebOs.Hosting;

namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            builder.AddPluginInfrastructure();

            builder.Logging.ConfigureLogging();

            builder.Services
                .ConfigureAuthentication(builder.Configuration)
                .ConfigureRateLimiting()
                .ConfigureOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.ConfigureScalarApiReference(builder.Configuration);
            }

            app.UseAuthentication();
            app.UseAuthorization();

            await app.LoadPluginsAsync();
            app.AddHandlers();

            app.Run();
        }
    }
}
