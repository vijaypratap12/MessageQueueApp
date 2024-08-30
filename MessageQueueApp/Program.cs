using MessageQueueApp.Business;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MessageQueueApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var producer = host.Services.GetRequiredService<Producer>();
            var consumer = host.Services.GetRequiredService<Consumer>();

            Console.WriteLine("Enter messages (type 'exit' to stop):");

            // Loop to accept multiple messages from the console
            while (true)
            {
                string input = Console.ReadLine();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Produce the message based on user input
                producer.Produce(input);
            }

            Console.WriteLine("Processing messages...");

            // Consume all the messages in the queue
            while (true)
            {
                var message = consumer.Consume();

                if (message == null)
                {
                    break;
                }
            }

            // Output processing results
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"Messages Processed Successfully: {consumer.GetSuccessCount()}");
            logger.LogInformation($"Errors Encountered: {consumer.GetErrorCount()}");
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddSingleton<IMessageQueue, MessageQueue>()
                            .AddSingleton<Producer>()
                            .AddSingleton<Consumer>()
                            .AddLogging(configure => configure.AddConsole()));
    }
}
