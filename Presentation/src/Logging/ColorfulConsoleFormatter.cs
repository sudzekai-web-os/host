using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Presentation.src.Extensions.String;

namespace Presentation.src.Logging
{
    public class ColorfulConsoleFormatter : ConsoleFormatter
    {
        public ColorfulConsoleFormatter(IOptionsMonitor<ColorfulConsoleFormatterOptions> options) 
            : base(FormatterName)
        {
            _options = options;
        }

        public const string FormatterName = "Toolkit";

        private readonly IOptionsMonitor<ColorfulConsoleFormatterOptions> _options;

        public override void Write<TState>(
            in LogEntry<TState> logEntry,
            IExternalScopeProvider? scopeProvider,
            TextWriter textWriter)
        {
            var options = _options.CurrentValue;

            var timestamp = DateTime.Now.ToString(options.TimestampFormat)
                .GetColoredString(options.TimestampForegroundColor)
                .GetColoredBackgroundString(options.TimestampBackgroundColor);

            string level = GetLogLevelColor(logEntry.LogLevel, options);

            var message = logEntry.Formatter(logEntry.State, logEntry.Exception)
                .GetColoredString(options.MessageForegroundColor)
                .GetColoredString(options.MessageBackgroundColor);

            var category = logEntry.Category
                .GetColoredString(options.CategoryForegroundColor)
                .GetColoredBackgroundString(options.CategoryBackgroundColor);

            textWriter.Write("{0} ", timestamp);
            textWriter.Write("{0} ", level);
            textWriter.Write("{0}", category);

            if (!options.SingleLine)
            {
                textWriter.WriteLine();
                var chars = message.ToCharArray();

                int lineLength = options.LineLength;

                for (int i = 0; i < chars.Length; i += lineLength)
                {
                    textWriter.WriteLine(
                        "{0}{1}",
                        new string(' ', 11),
                        string.Join("", chars.Skip(i).Take(lineLength)));
                }
            }
            else
            {
                textWriter.WriteLine(" {0}", message);
            }

            if (logEntry.Exception is not null)
            {
                textWriter.WriteLine(logEntry.Exception);
            }
        }

        private static string GetLogLevelColor(LogLevel logLevel, ColorfulConsoleFormatterOptions options)
            => logLevel switch
            {
                LogLevel.Trace => "TRCE".GetColoredString(options.TraceLevelForegroundColor)
                                        .GetColoredBackgroundString(options.TraceLevelBackgroundColor),

                LogLevel.Debug => "DBUG".GetColoredString(options.DebugLevelForegroundColor)
                                        .GetColoredBackgroundString(options.DebugLevelBackgroundColor),

                LogLevel.Information => "INFO".GetColoredString(options.InformationLevelForegroundColor)
                                              .GetColoredBackgroundString(options.InformationLevelBackgroundColor),

                LogLevel.Warning => "WARN".GetColoredString(options.WarningLevelForegroundColor)
                                          .GetColoredBackgroundString(options.WarningLevelBackgroundColor),

                LogLevel.Error => "ERR".GetColoredString(options.ErrorLevelForegroundColor)
                                       .GetColoredBackgroundString(options.ErrorLevelBackgroundColor),

                LogLevel.Critical => "CRIT".GetColoredString(options.CriticalLevelForegroundColor)
                                           .GetColoredBackgroundString(options.CriticalLevelBackgroundColor),
                _ => "None"
            };
    }
}
