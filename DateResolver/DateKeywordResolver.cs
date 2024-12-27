using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace DateResolver;

public class DateKeywordResolver
{
    private static readonly Regex KeywordPattern = new Regex(
        @"(?i)\b(NOW|YESTERDAY|TODAY)([+-]\d+[dhms])?\b|Format\((NOW|TODAY),\s*""([^""]+)""\)",
        RegexOptions.Compiled);

    private readonly ILogger<DateKeywordResolver> _logger;

    public DateKeywordResolver(ILogger<DateKeywordResolver> logger)
    {
        _logger = logger;
    }

    public static string Resolve(string input, CultureInfo culture = null)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));

        culture ??= CultureInfo.InvariantCulture;

        return KeywordPattern.Replace(input, match =>
        {
            string keyword = match.Groups[1].Success ? match.Groups[1].Value.ToUpperInvariant() : null;
            string offset = match.Groups[2].Value;
            string formatKeyword = match.Groups[3].Success ? match.Groups[3].Value.ToUpperInvariant() : null;
            string format = match.Groups[4].Value;

            DateTime baseDate;
            if (keyword != null)
            {
                baseDate = keyword switch
                {
                    "NOW" => DateTime.UtcNow,
                    "YESTERDAY" => DateTime.UtcNow.AddDays(-1),
                    "TODAY" => DateTime.UtcNow.Date,
                    _ => throw new InvalidOperationException($"Unsupported keyword: {keyword}")
                };
            }
            else if (formatKeyword != null)
            {
                baseDate = formatKeyword switch
                {
                    "NOW" => DateTime.UtcNow,
                    "TODAY" => DateTime.UtcNow.Date,
                    _ => throw new InvalidOperationException($"Unsupported keyword in format: {formatKeyword}")
                };

                if (!string.IsNullOrEmpty(format))
                {
                    try
                    {
                        return baseDate.ToString(format, culture);
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException($"Invalid date format: \"{format}\"", ex);
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("No valid keyword or format found.");
            }

            if (!string.IsNullOrEmpty(offset))
                baseDate = ApplyOffset(baseDate, offset);

            return baseDate.ToString("yyyyMMdd", culture);
        });
    }


    private static DateTime ApplyOffset(DateTime date, string offset)
    {
        if (string.IsNullOrWhiteSpace(offset) || offset.Length < 2)
            throw new FormatException($"Invalid offset format: {offset}");

        char unit = offset[^1];
        if (!int.TryParse(offset[..^1], out int value))
            throw new FormatException($"Invalid offset format: {offset}");

        return unit switch
        {
            'd' => date.AddDays(value),
            'h' => date.AddHours(value),
            'm' => date.AddMinutes(value),
            's' => date.AddSeconds(value),
            _ => throw new FormatException($"Unsupported offset unit: {unit}")
        };
    }


}
