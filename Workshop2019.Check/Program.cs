using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Workshop2019.Check.JsonPlaceholderApi;

namespace Workshop2019.Check
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var api = new JsonPlaceholderApiHttpClient(
                apiUrl: new Uri(configuration["JsonPlaceholderApiUrl"]),
                apiKey: configuration["JsonPlaceholderApiKey"]
            );

            var todoItems = await api.Get<List<TodoItem>>("todos", new Dictionary<string, string>
            {
                { "userId", "1" },
                { "completed", "true" }
            });

            Console.WriteLine("User 1 todos:");
            Console.WriteLine();

            foreach (var todoItem in todoItems)
            {
                Console.WriteLine($"Id:        {todoItem.Id}");
                Console.WriteLine($"Title:     {todoItem.Title}");
                Console.WriteLine($"Completed: {todoItem.Completed}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
