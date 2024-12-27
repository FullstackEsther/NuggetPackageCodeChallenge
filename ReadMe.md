# DateResolver

DateResolver is a lightweight NuGet package designed to dynamically resolve date-related keywords in file names. It simplifies automation tasks by allowing you to use intuitive placeholders like `NOW`, `YESTERDAY`, and relative offsets such as `NOW-1d` in file names.

---

## Features

- **Keyword Support**:
  - Predefined keywords: `NOW`, `YESTERDAY`, `TODAY`.
  - Relative offsets: `NOW-1d`, `NOW+2h`, etc.
  - Date formatting with patterns: `Format(NOW, "yyyy-MM-dd")`.

- **Localization**:
  - Handles date and time formatting based on specified cultures (`CultureInfo`).

- **Lightweight**:
  - Minimal dependencies, designed for quick integration and high performance.

---

## Installation

Install DateResolver from NuGet:

```bash
Install-Package DateResolver
```

Or use the .NET CLI:

```bash
dotnet add package DateResolver
```

---

## Usage

### Basic Example

```csharp
using DateResolver;

string input = "data-NOW.txt";
string resolved = DateKeywordResolver.Resolve(input);
Console.WriteLine(resolved); // Outputs: data-20241227.txt (based on current date)
```

### Using Relative Offsets

```csharp
string input = "backup-NOW-1d.zip";
string resolved = DateKeywordResolver.Resolve(input);
Console.WriteLine(resolved); // Outputs: backup-20241226.zip (based on current date)
```

### Custom Date Formats

```csharp
string input = "log-Format(NOW, \"yyyy-MM-dd\").txt";
string resolved = DateKeywordResolver.Resolve(input);
Console.WriteLine(resolved); // Outputs: log-2024-12-27.txt
```

### Localization

```csharp
using System.Globalization;

string input = "report-Format(NOW, \"dd MMM yyyy\").txt";
CultureInfo frenchCulture = new CultureInfo("fr-FR");
string resolved = DateKeywordResolver.Resolve(input, frenchCulture);
Console.WriteLine(resolved); // Outputs: report-27 déc. 2024.txt
```

---

## API Reference

### `DateKeywordResolver.Resolve`
Resolves date-related keywords and placeholders in a string.

```csharp
string Resolve(string input, CultureInfo culture = null);
```

- **Parameters**:
  - `input`: The input string containing date-related keywords.
  - `culture`: Optional. Specifies the culture for date formatting. Defaults to `CultureInfo.InvariantCulture`.

- **Returns**:
  - The resolved string with placeholders replaced by date values.

### `DateKeywordResolver.ApplyOffset`
Applies relative offsets to a base `DateTime`.

```csharp
DateTime ApplyOffset(DateTime baseDate, string offset);
```

- **Parameters**:
  - `baseDate`: The base `DateTime` value.
  - `offset`: The offset string (e.g., `+2h`, `-1d`).

- **Returns**:
  - A new `DateTime` adjusted by the specified offset.

---

## Error Handling

DateResolver provides:
- **Invalid Input Handling**: Ensures input strings are non-empty and contain valid keywords.
- **Offset Validation**: Throws detailed exceptions for unsupported or invalid offsets.
- **Keyword Validation**: Provides feedback for unrecognized keywords.

---

## Examples

### FTP File Processing
Resolve dynamic file names for FTP downloads:

```csharp
string ftpFileName = "data-NOW-1d.csv";
string resolvedFileName = DateKeywordResolver.Resolve(ftpFileName);
ftpDownloader.Download(resolvedFileName);
```

### Generating Log File Names
Generate log file names dynamically for daily logging:

```csharp
string logFile = "logs/log-Format(NOW, \"yyyy-MM-dd\").txt";
string resolvedLogFile = DateKeywordResolver.Resolve(logFile);
Console.WriteLine(resolvedLogFile);
```

---

## Future Enhancements

- Add support for custom user-defined keywords.
- Extend localization to include specific time zones.
- Include caching for optimized performance in repetitive parsing scenarios.

---

## License

DateResolver is licensed under the MIT License. See the `LICENSE` file for more details.

---

## Contributing

Contributions are welcome! Feel free to fork the repository and submit a pull request with your changes.

---


