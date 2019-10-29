using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Dapper.ViewModels;

namespace TeduCoreApp.Application.Dapper.Interfaces
{
    public interface IProductReportService
    {
        Task<IEnumerable<ProductReportViewModel>> GetProductReport(string fromDate, string toDate);
    }
}
