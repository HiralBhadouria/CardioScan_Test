using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ReadFile.Model
{
    public class Record
    {
        public string TaskId { get; set; }
        public string CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string JobId { get; set; }
    }
}
