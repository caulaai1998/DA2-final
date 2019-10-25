using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Dapper.Interfaces;
using TeduCoreApp.Application.Dapper.ViewModels;

namespace TeduCoreApp.Application.Dapper.Implementation
{
    public class UserReportService : IUserReportService
    {
        private readonly IConfiguration _configuration;

        public UserReportService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserReportViewModel>> GetReport(string fromDate, string toDate)
        {

            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                var now = DateTime.Now;

                var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                dynamicParameters.Add("@fromDate", string.IsNullOrEmpty(fromDate) ? firstDayOfMonth.ToString("MM/dd/yyyy") : fromDate);
                dynamicParameters.Add("@toDate", string.IsNullOrEmpty(toDate) ? lastDayOfMonth.ToString("MM/dd/yyyy") : toDate);

                try
                {
                    return await sqlConnection.QueryAsync<UserReportViewModel>(
                        "GetTotalNewUser", dynamicParameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
