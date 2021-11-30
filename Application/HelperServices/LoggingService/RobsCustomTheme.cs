using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Sinks.SystemConsole.Themes;
namespace Application.HelperServices.LoggingService {
    public class RobsCustomTheme : ConsoleTheme {
        private readonly IReadOnlyDictionary<ConsoleThemeStyle, string> styles;
        public static RobsCustomTheme Theme { get; } = CustomConsoleThemes.VisualStudioMacLight;

        public RobsCustomTheme(IReadOnlyDictionary<ConsoleThemeStyle, string> _styles) {
            if (_styles == null) {
                throw new ArgumentNullException(nameof(_styles));
            }

            this.styles = _styles.ToDictionary(_kv => _kv.Key, _kv => _kv.Value);
        }

        public override bool CanBuffer => true;

        protected override int ResetCharCount { get; } = "\x001B[0m".Length;

        public override int Set(TextWriter _output, ConsoleThemeStyle _style) {
            string str;
            if (!styles.TryGetValue(_style, out str))
                return 0;
            _output.Write(str);
            return str.Length;
        }

        public override void Reset(TextWriter output) {
            output.Write("\x001B[0m");
        }
    }
}
