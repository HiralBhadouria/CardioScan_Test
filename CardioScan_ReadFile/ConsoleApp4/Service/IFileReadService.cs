using ConsoleApp_ReadFile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ReadFile.Service
{
    public interface IFileReadService
    {
        void LoadData(string filePath);

        List<Record> QueryByCustomerJobDate(string customerId, string jobId, DateTime date);

        int CountByCustomerId(string customerId);
    }
}
