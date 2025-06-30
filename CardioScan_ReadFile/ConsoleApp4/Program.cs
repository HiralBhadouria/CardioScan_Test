using ConsoleApp_ReadFile.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TaskDataQueryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IFileReadService, FileReadService>()
            .BuildServiceProvider();

            try
            {
                var filePath = @"C:\\CodeChallenge\\taskData.csv";
                var readService = serviceProvider.GetRequiredService<IFileReadService>();

                Console.WriteLine("Loading data...");
                readService.LoadData(filePath);
                Console.WriteLine("Data loaded successfully.\n");

                string choice;
                do
                {
                    Console.WriteLine("\nSelect an option:");
                    Console.WriteLine("1 - Query by customerId, jobId and date");
                    Console.WriteLine("2 - Count records by customerId");
                    Console.WriteLine("0 - Exit");
                    Console.Write("Enter choice (0, 1 or 2): ");
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            RunQuery1(readService);
                            break;

                        case "2":
                            RunQuery2(readService);
                            break;

                        case "0":
                            Console.WriteLine("Exiting application...");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please enter 0, 1 or 2.");
                            break;
                    }

                } while (choice != "0");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to close...");
            Console.ReadKey();
        }

        static void RunQuery1(IFileReadService readService)
        {
            Console.Write("Enter customerId: ");
            var customerId = Console.ReadLine()?.Trim();

            Console.Write("Enter jobId: ");
            var jobId = Console.ReadLine()?.Trim();

            Console.Write("Enter date (dd/MM/yyyy): ");
            var dateInput = Console.ReadLine()?.Trim();

            if (!DateTime.TryParseExact(dateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            var filtered = readService.QueryByCustomerJobDate(customerId, jobId, date);
            Console.WriteLine($"Found {filtered.Count()} record(s):");
            foreach (var record in filtered)
            {
                Console.WriteLine("CustomeId :" + record.CustomerId + " Job Id: " + record.JobId + " Date : " + record.Date);
            }
        }

        static void RunQuery2(IFileReadService readService)
        {
            Console.Write("Enter customerId: ");
            var customerId = Console.ReadLine()?.Trim();
            int count = readService.CountByCustomerId(customerId);
            Console.WriteLine($"Total records for customerId '{customerId}': {count}");
        }
    }
}