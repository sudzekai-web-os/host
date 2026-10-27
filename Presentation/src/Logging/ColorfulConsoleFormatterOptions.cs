using Microsoft.Extensions.Logging.Console;
using Presentation.src.Types.Enums;
using System.Drawing;

namespace Presentation.src.Logging
{
    public class ColorfulConsoleFormatterOptions : ConsoleFormatterOptions
    {
        public FormatterColor TraceLevelForegroundColor { get; set; } = FormatterColor.Black;

        public FormatterColor TraceLevelBackgroundColor { get; set; } = FormatterColor.White;

        public FormatterColor DebugLevelForegroundColor { get; set; } = FormatterColor.Default;

        public FormatterColor DebugLevelBackgroundColor { get; set; } = FormatterColor.Default;

        public FormatterColor InformationLevelForegroundColor { get; set; } = FormatterColor.Blue;

        public FormatterColor InformationLevelBackgroundColor { get; set; } = FormatterColor.Default;

        public FormatterColor WarningLevelForegroundColor { get; set; } = FormatterColor.DarkYellow;

        public FormatterColor WarningLevelBackgroundColor { get; set; } = FormatterColor.Default;

        public FormatterColor ErrorLevelForegroundColor { get; set; } = FormatterColor.Red;

        public FormatterColor ErrorLevelBackgroundColor { get; set; } = FormatterColor.Default;

        public FormatterColor CriticalLevelForegroundColor { get; set; } = FormatterColor.White;

        public FormatterColor CriticalLevelBackgroundColor { get; set; } = FormatterColor.Red;

        public FormatterColor MessageForegroundColor { get; set; } = FormatterColor.White;

        public FormatterColor MessageBackgroundColor { get; set; } = FormatterColor.Default;

        public FormatterColor CategoryForegroundColor { get; set; } = FormatterColor.DarkGray;

        public FormatterColor CategoryBackgroundColor { get; set; } = FormatterColor.Default;

        public FormatterColor TimestampForegroundColor { get; set; } = FormatterColor.Default;

        public FormatterColor TimestampBackgroundColor { get; set; } = FormatterColor.Default;

        public int LineLength { get; set; } = int.MaxValue;

        public bool SingleLine { get; set; } = false;
    }
}
