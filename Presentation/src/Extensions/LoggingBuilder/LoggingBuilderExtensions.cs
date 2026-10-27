using Presentation.src.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.src.Extensions.LoggingBuilder
{
    internal static class LoggingBuilderExtensions
    {
        [UnconditionalSuppressMessage("Trimming", "IL2026")]
        [UnconditionalSuppressMessage("AOT", "IL3050")]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ColorfulConsoleFormatter))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ColorfulConsoleFormatterOptions))]
        public static ILoggingBuilder ConfigureLogging(this ILoggingBuilder builder)
        {
            builder.AddConsoleFormatter<ColorfulConsoleFormatter, ColorfulConsoleFormatterOptions>(o =>
            {
                o.TimestampFormat = "[HH:mm:ss]";
            });

            builder.ClearProviders();
            builder.AddConsole(c =>
            {
                c.FormatterName = "Toolkit";
            });

            return builder;
        }
    }
}
