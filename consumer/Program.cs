using Confluent.Kafka;
using consumer.Data;
using consumer.Models;
using consumer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Consumer;

class Program
{
    static void Main(string[] args)
    {
        var connectionString = "Server=localhost;Database=testDb;User=root;Password=root";
        var services = new ServiceCollection();

        services.AddDbContext<consumerDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<consumerDbContext>();
        services.AddScoped<DataProcessing>();
        var serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<consumerDbContext>();
            db.Database.EnsureCreated();
        }

        var bootstrap = "localhost:9092";
        var group = "vi";

        var consumerConfig = new ConsumerConfig
        {
            GroupId = group,
            BootstrapServers = bootstrap,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();

        var topics = new[] { "PerimeterSensor-topic", "UAV-topic" };

        Console.WriteLine("Consumer started. Processing topics sequentially...");

        while (true)
        {
            foreach (var topic in topics)
            {
                Console.WriteLine($"\n--- Switching to topic: {topic} ---");
                consumer.Subscribe(topic);

                while (true)
                {
                    try
                    {
                        var result = consumer.Consume(TimeSpan.FromSeconds(20));

                        if (result == null)
                        {
                            Console.WriteLine($"No new messages on [{topic}] for 10 seconds. Moving to next topic.");
                            break;
                        }

                        Console.WriteLine($"Received message from [{result.Topic}]: {result.Message.Value}");
                        var v = new Validations();

                        using var scope = serviceProvider.CreateScope();
                        var processing = scope.ServiceProvider.GetRequiredService<DataProcessing>();
                        bool success = false;
                        List<string> strings = new List<string>();
                        if (result.Topic == "PerimeterSensor-topic")
                        {
                            if (result.Message.Value.ToLower().Contains("bad"))
                            {
                                strings.Add("nini");
                            }
                            else if (result.Message.Value.ToLower().Contains("good"))
                            {
                                strings.Add("true");
                            }
                            else
                            {
                                strings.Add("false");
                            }
                            success = processing.LiveStatusToDb(result.Message.Value, strings);
                        }
                        else if (result.Topic == "UAV-topic")
                        {
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };
                            var json = JsonSerializer.Deserialize<LiveStatus>(result.Message.Value, options);
                            if (v.ValidUAV(json).Contains("nini"))
                            {
                                strings.Add("nini");
                            }
                            else if (v.ValidUAV(json).Contains("good"))
                            {
                                strings.Add("true");
                            }
                            else
                            {
                                strings.Add("false");
                            }
                            success = processing.LiveStatusToDb(result.Message.Value, strings);
                        }
                        if (success)
                        {
                            consumer.Commit(result);
                            Console.WriteLine($"-> Successfully saved and committed message from [{result.Topic}]!");
                        }
                        else
                        {
                            Console.WriteLine($"-> Failed to process message from [{result.Topic}].");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Exception caught: {ex.Message}");
                        if (ex.InnerException != null)
                        {
                            Console.WriteLine($"[Inner ERROR]: {ex.InnerException.Message}");
                        }
                        Thread.Sleep(500);
                    }
                }

                consumer.Unsubscribe();
            }

            Console.WriteLine("\n--- Finished all topics. Restarting cycle from the beginning in 5 seconds... ---");
            Thread.Sleep(30);
        }
    }
}