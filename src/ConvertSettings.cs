using Spectre.Console.Cli;

public class ConvertSettings : CommandSettings
{
    [CommandOption("-i|--input")]
    public string InputPath { get; init; } = Directory.GetCurrentDirectory();

    [CommandOption("-o|--output")]
    public string OutputPath { get; init; } = Directory.GetCurrentDirectory();

    [CommandOption("-e|--exclude")]
    public string[] ExcludeFiles { get; init; }

    [CommandOption("-f|--files")]
    public string[] Files { get; init; }
}
