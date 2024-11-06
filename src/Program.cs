using Spectre.Console.Cli;

// Configure and run the command application
var app = new CommandApp<ConvertCommand>();
app.Configure(config =>
{
    config.SetApplicationName("tps-to-csv-converter");
});

// Run the application with command-line arguments
await app.RunAsync(args);
