using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Dapper.ViewModels;

namespace TeduCoreApp.Application.Dapper.Interfaces
{
    public interface IUserReportService
    {
        Task<IEnumerable<UserReportViewModel>> GetReport(string fromDate, string toDate);
    }
}
