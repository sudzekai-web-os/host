using Presentation.src.Extensions.LoggingBuilder;
using Presentation.src.Extensions.ServiceCollection;
using Scalar.AspNetCore;
using Sudzekai.LinCtrl.Hosting;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            var portSetting = builder.Configuration["LinCtrl:Port"] ?? "5199";
            if (!int.TryParse(portSetting, out var port) || port is < 1 or > 65535)
                throw new InvalidOperationException("LinCtrl:Port must be an integer between 1 and 65535");

            builder.Configuration["LinCtrl:Port"] = portSetting;
            builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(port));
            builder.Logging.ConfigureLogging();
            builder.AddLinCtrlInfrastructure();

            var openApiEnabled = builder.Configuration.GetValue<bool>("OpenApi:IsEnabled");
            if (openApiEnabled)
                builder.Services.ConfigureOpenApi();

            var app = builder.Build();

            app.MapLinCtrlInfrastructure();
            app.LoadPlugins();
            app.InitializePlugins();

            if (openApiEnabled)
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options => options.AddPreferredSecuritySchemes(ServiceCollectionOpenApiExtensions.BearerScheme));
            }

            app.Run();
        }
    }
}
