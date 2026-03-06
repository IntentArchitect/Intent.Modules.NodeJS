using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intent.Modules.Npm;

internal class NpmVersionHelper
{
    public static int VersionToInt(string version)
    {
        if (string.IsNullOrWhiteSpace(version))
            return 0;

        var s = version.Trim();

        // Take first token if ranges/extra text exist (e.g., ">=20 <22")
        s = s.Split(["||"], StringSplitOptions.None)[0].Trim();
        s = s.Split([' ', '\t', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries)[0].Trim();

        // Strip common prefixes/operators: ^ ~ = v
        while (s.Length > 0 && (s[0] == '^' || s[0] == '~' || s[0] == '=')) s = s[1..].TrimStart();
        if (s.StartsWith("v", StringComparison.OrdinalIgnoreCase)) s = s[1..];

        // Drop build metadata and prerelease
        int plus = s.IndexOf('+');
        if (plus >= 0) s = s[..plus];
        int dash = s.IndexOf('-');
        if (dash >= 0) s = s[..dash];

        // Parse core x.y.z (missing parts => 0)
        var parts = s.Split('.', StringSplitOptions.RemoveEmptyEntries);
        int major = parts.Length > 0 ? ParseIntOrZero(parts[0]) : 0;
        int minor = parts.Length > 1 ? ParseIntOrZero(parts[1]) : 0;
        int patch = parts.Length > 2 ? ParseIntOrZero(parts[2]) : 0;

        // Pack (fits int as long as each part < 1000 and major < 2147)
        return checked(major * 1_000_000 + minor * 1_000 + patch);
    }

    private static int ParseIntOrZero(string s) =>
        int.TryParse(s, out var n) ? n : 0;
}
