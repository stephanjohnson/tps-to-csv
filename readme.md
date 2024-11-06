# TPS to CSV Converter CLI

This project is a CLI tool for converting Clarion TPS files to CSV format. It uses [Spectre.Console](https://spectreconsole.net/) and [Spectre.Console.Cli](https://github.com/spectreconsole/spectre.console) to provide a rich command-line interface with multi-select capabilities, making it easy to select TPS files for conversion. The CSV output files are generated using [CsvHelper](https://joshclose.github.io/CsvHelper/).

## Features
- **TPS to CSV Conversion**: Converts selected TPS files to CSV format.
- **Multi-Select Prompt**: If no CLI arguments are provided, a prompt lists all TPS files in the current path, allowing you to select which files to convert.
- **File Matching**: CSV files match the name of the input TPS file, with `.csv` as the extension.
- **Customizable Paths**: Specify input and output paths via CLI arguments, with defaults to the current path.
- **File Exclusion/Inclusion**: Specify files to explicitly include or exclude from the conversion.

## Installation

Clone the repository and navigate to the project directory.

```bash
git clone https://github.com/yourusername/tps-to-csv-converter.git
cd tps-to-csv-converter
```

Restore the required packages:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

## Usage

Run the application with the following syntax:

```bash
dotnet run --project TpsExtract -- [options]
```

### Options

- `-i`, `--input` - Path to the directory containing the TPS files. Defaults to the current directory.
- `-o`, `--output` - Path to the directory where CSV files will be saved. Defaults to the current directory.
- `-e`, `--exclude` - Specify files to exclude from the conversion.
- `-f`, `--files` - Specify files to include for conversion.

### Example Commands

#### Convert all TPS files in the current directory:

```bash
dotnet run --project TpsExtract
```

#### Specify input and output directories:

```bash
dotnet run --project TpsExtract -- -i ./input -o ./output
```

#### Include specific files:

```bash
dotnet run --project TpsExtract -- -f file1.tps file2.tps
```

#### Exclude specific files:

```bash
dotnet run --project TpsExtract -- -e file3.tps file4.tps
```

## Project Structure

- `Program.cs`: Main entry point using top-level statements.
- `ConvertCommand.cs`: Command logic for handling TPS to CSV conversion.
- `ConvertSettings.cs`: CLI settings for managing input, output, exclude, and include options.

## Dependencies

- **CsvHelper**: For CSV writing functionality.
- **Spectre.Console**: For rich CLI visuals and multi-select prompts.
- **TpsParse**: For parsing Clarion TPS files.

## License

This project is licensed under the MIT License.
