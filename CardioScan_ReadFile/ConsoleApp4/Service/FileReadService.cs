using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_ReadFile.Model;

namespace ConsoleApp_ReadFile.Service
{
    internal class FileReadService : IFileReadService
    {
        private List<Record> _records = new List<Record>();

        public void LoadData(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV file not found.", filePath);

            var lines = File.ReadAllLines(filePath);
            if (lines.Length <= 1)
                throw new InvalidOperationException("CSV file is empty or only contains headers.");

            _records = new List<Record>();

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');

                if (parts.Length != 4)
                {
                    Console.WriteLine($"Skipping invalid line {i + 1}: {lines[i]}");
                    continue;
                }

                if (!DateTime.TryParseExact(parts[2].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    Console.WriteLine($"Invalid date format on line {i + 1}, skipping: {lines[i]}");
                    continue;
                }

                _records.Add(new Record
                {
                    TaskId = parts[0].Trim(),
                    CustomerId = parts[1].Trim(),
                    Date = date,
                    JobId = parts[3].Trim()
                });
            }
        }

        public List<Record> QueryByCustomerJobDate(string customerId, string jobId, DateTime date)
        {
            return _records.Where(r =>
                r.CustomerId == customerId &&
                r.JobId == jobId &&
                r.Date.Date == date.Date).ToList();
        }

        public int CountByCustomerId(string customerId)
        {
            return _records.Count(r => r.CustomerId.Equals(customerId));
        }
    }
}
