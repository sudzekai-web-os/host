using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace Presentation.src.Extensions.WebApplication
{
    internal static class WebApplicationScalarExtensions
    {
        public static void ConfigureScalarApiReference(this Microsoft.AspNetCore.Builder.WebApplication app, IConfiguration configuration)
        {
            app.MapScalarApiReference(o =>
            {
                o.WithTitle("LinCtrl API");
                o.AddPreferredSecuritySchemes(JwtBearerDefaults.AuthenticationScheme);
                o.EnablePersistentAuthentication();
                o.BaseServerUrl = "http://localhost:5199";
            });
        }
    }
}