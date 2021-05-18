using System;
using CodeAssignmentPeerIslands.Service;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using CodeAssignmentPeerIslands.Infra;

namespace CodeAssignmentPeerIslands
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

                var path = configuration.GetSection("FilePath");
                var parsedFile = await (new ReadJsonFile().ConvertJsonToObject(path.Value));
                ServiceCreateSqlServerScript service = new ServiceCreateSqlServerScript();
                foreach (var table in parsedFile.Queries)
                {
                    try
                    {
                        string queryCreated = await service.CreateQueryAsync(table);
                        Console.WriteLine($"-------QUERY FOR TABLE {table.Name}-------");
                        Console.WriteLine(queryCreated);
                        Console.WriteLine($"-------END-------");
                        Console.WriteLine();
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"[Error] => While creating query for {table.Name} - \n Current error message: ${error.Message}");
                        Console.WriteLine();
                    }
                }
                Console.ReadKey();
            } catch(Exception error)
            {
                Console.WriteLine($"[Error] => Something went wrong. \n Check the original error: ${error.Message}");
                Console.WriteLine();
            }
        }
    }
}
