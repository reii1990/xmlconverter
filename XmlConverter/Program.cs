using Microsoft.Extensions.Logging;
using XmlConverter;

var input = args.Length > 0 ? args[0] : null;
var output = args.Length > 1 ? args[1] : null;

if (input == null)
{
    Console.WriteLine("Please enter path to input file:");
    input = Console.ReadLine();
}

if (output == null) 
{
    Console.WriteLine("Please enter path to output file:");
    output = Console.ReadLine();
}

if (input != null && output != null)
{

    using var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });
    ILogger logger = loggerFactory.CreateLogger<Program>();
    logger.LogInformation("Example log message");
    new Converter(logger).Convert(input, output);
}
else 
{
    Console.WriteLine("Input and output files has to be specified");
}