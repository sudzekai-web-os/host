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

            if (IsOpenApiEnabled(builder.Configuration))
                builder.Services.AddOpenApi();
            
            builder.Logging.ConfigureLogging();

            builder.Services
                .ConfigureAuthentication(builder.Configuration)
                .ConfigureRateLimiting()
                .ConfigureOpenApi();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            await app.LoadPluginsAsync();
            app.MapHandlers();

            if (IsOpenApiEnabled(app.Configuration))
                app.ConfigureScalarApiReference(builder.Configuration);

            app.Run();
        }

        private static bool IsOpenApiEnabled(IConfiguration configuration)
            => configuration["OpenApi:IsEnabled"] is var openApiEnabledStr
                && Convert.ToBoolean(openApiEnabledStr) is var isOpenApiEnabled
                && isOpenApiEnabled;
    }
}
