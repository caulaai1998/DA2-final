using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Dapper.Interfaces;
using TeduCoreApp.Extensions;

namespace TeduCoreApp.Areas.Admin.Controllers
{
 
    public class HomeController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IUserReportService _userReportService;

        public HomeController(IReportService reportService,IUserReportService userReportService)
        {
            _reportService = reportService;
            _userReportService = userReportService;
        }

        public IActionResult Index()
        {
            var email = User.GetSpecificClaim("Email");

            return View();
        }

        public async Task<IActionResult> GetRevenue(string fromDate, string toDate)
        {
            return new OkObjectResult(await _reportService.GetReportAsync(fromDate, toDate));
        }

        public async Task<IActionResult> GetNewUser(string fromDate, string toDate)
        {
            return new OkObjectResult(await _userReportService.GetReport(fromDate, toDate));
        }
    }
}