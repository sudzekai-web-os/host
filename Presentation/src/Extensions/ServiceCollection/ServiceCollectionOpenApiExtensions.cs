using Microsoft.OpenApi;

namespace Presentation.src.Extensions.ServiceCollection
{
    internal static class ServiceCollectionOpenApiExtensions
    {
        public const string BearerScheme = "Bearer";

        public static IServiceCollection ConfigureOpenApi(this IServiceCollection services)
            => services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, _, _) =>
                {
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                    document.Components.SecuritySchemes[BearerScheme] = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT"
                    };

                    document.Security ??= [];
                    document.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference(BearerScheme, document)] = []
                    });

                    return Task.CompletedTask;
                });
            });
    }
}
