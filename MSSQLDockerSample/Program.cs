using System;
using System.Threading.Tasks;

namespace MSSQLDockerSample
{
    class Program
    {
        private const string ConnectionString = "Server=tcp:localhost,1433;User id=sa;Password=Password_123;Database=Test";

        private static readonly Storage storage = new Storage(ConnectionString);

        static async Task Main(string[] args)
        {
            Console.WriteLine("Type 'save TEXT' to store a record. Type 'list' to list all records documents. Type 'q' to quit.");

            bool shouldQuit = false;

            do
            {
                var input = Console.ReadLine();

                (var command, var argument) = ParseInput(input);

                switch (command.ToLower())
                {
                    case "save":
                        await storage.SaveAsync(argument);
                        Console.WriteLine("Record has been saved");
                        break;

                    case "list":
                        var records = await storage.ListAsync();
                        foreach (var record in records)
                        {
                            Console.WriteLine(record);
                        }
                        break;

                    case "q":
                        shouldQuit = true;
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            } while (!shouldQuit);
        }

        private static (string, string) ParseInput(string input)
        {
            var separatorIndex = input.IndexOf(' ');
            if (separatorIndex < 0)
            {
                return (input, null);
            }
            var command = input.Substring(0, separatorIndex);
            var argument = input.Substring(separatorIndex + 1);
            return (command, argument);
        }
    }
}
