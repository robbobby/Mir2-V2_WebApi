using Serilog.Sinks.SystemConsole.Themes;
using System.Collections.Generic;

namespace LoggingService {
    public static class CustomConsoleThemes {
        
        // These colours in the ansi nuget package make no sense
        public static RobsCustomTheme VisualStudioMacLight { get; } =
            new RobsCustomTheme(new Dictionary<ConsoleThemeStyle, string> {
                [ConsoleThemeStyle.Text] = "\u001b[30m",
                [ConsoleThemeStyle.SecondaryText] = "\u001b[30m",
                [ConsoleThemeStyle.TertiaryText] = "\u001b[30m",
                [ConsoleThemeStyle.Invalid] = "\u001b[30m",
                [ConsoleThemeStyle.Null] = "\u001b[30m",
                [ConsoleThemeStyle.Name] = "\u001b[30m",
                [ConsoleThemeStyle.String] = "\u001b[30m",
                [ConsoleThemeStyle.Number] = "\u001b[30m",
                [ConsoleThemeStyle.Boolean] = "\u001b[30m",
                [ConsoleThemeStyle.Scalar] = "\u001b[30m",
                [ConsoleThemeStyle.LevelVerbose] = "\u001b[30m",
                [ConsoleThemeStyle.LevelDebug] = $"{AnsiCodes.Background.Black}{AnsiCodes.Color.White}",
                [ConsoleThemeStyle.LevelInformation] = $"{AnsiCodes.Color.Black}",
                [ConsoleThemeStyle.LevelWarning] = $"{AnsiCodes.Background.Yellow}{AnsiCodes.Color.Black}",
                [ConsoleThemeStyle.LevelError] = $"{AnsiCodes.Background.DrkGray}{AnsiCodes.Color.White}",
                [ConsoleThemeStyle.LevelFatal] = $"{AnsiCodes.Background.Red}{AnsiCodes.Color.Black}"
            });

    }
}
