using Spectre.Console;
using Spectre.Console.Cli;
using CsvHelper;
using TpsParse.Tps;
using System.Globalization;

public class ConvertCommand : AsyncCommand<ConvertSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, ConvertSettings settings)
    {
        // Get all TPS files in the input path
        var allFiles = Directory.GetFiles(settings.InputPath, "*.tps")
                                .Select(Path.GetFileName)
                                .ToList();

        // Exclude specified files
        var filesToConvert = allFiles
            .Where(file => settings.ExcludeFiles == null || !settings.ExcludeFiles.Contains(file))
            .ToList();

        // Handle user selection if no files specified explicitly
        if (settings.Files == null || settings.Files.Length == 0)
        {
            filesToConvert = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Select the [green]TPS[/] files to convert to CSV")
                    .PageSize(10)
                    .AddChoices(filesToConvert));
        }
        else
        {
            filesToConvert = filesToConvert
                .Where(file => settings.Files.Contains(file))
                .ToList();
        }

        foreach (var file in filesToConvert)
        {
            var inputFile = Path.Combine(settings.InputPath, file);
            var outputFile = Path.Combine(settings.OutputPath, Path.ChangeExtension(file, ".csv"));
            ConvertTpsToCsv(inputFile, outputFile);
        }

        AnsiConsole.MarkupLine("[green]Conversion completed successfully![/]");
        return 0;
    }

    private void ConvertTpsToCsv(string inputFilePath, string outputFilePath)
    {
        try
        {
            using var file = File.Open(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var tps = new TpsFile(file);
            var definitions = tps.GetTableDefinitions();

            if (definitions.Count > 1)
            {
                AnsiConsole.MarkupLine($"[red]File '{inputFilePath}' contains multiple tables, skipping.[/]");
                return;
            }

            using var writer = new StreamWriter(outputFilePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            foreach (var field in definitions[0].Fields)
            {
                var name = field.FieldName.Substring(field.FieldName.IndexOf(":") + 1);
                if (field.IsArray())
                {
                    for (var i = 0; i < field.Elements; i++)
                        csv.WriteField($"{name}[{i}]");
                    continue;
                }
                csv.WriteField(name);
            }
            csv.NextRecord();

            foreach (var record in tps.GetDataRecords(definitions[0]))
            {
                foreach (var value in record.Values)
                {
                    if (value is IEnumerable<object> array)
                    {
                        foreach (var arrayValue in array)
                            csv.WriteField(arrayValue);
                    }
                    else
                    {
                        csv.WriteField(value);
                    }
                }
                csv.NextRecord();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error processing '{inputFilePath}': {ex.Message}[/]");
        }
    }
}
