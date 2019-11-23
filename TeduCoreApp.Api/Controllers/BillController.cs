using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeduCoreApp.Api.Controllers
{
    public class BillController : ApiController
    {
        IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            return new OkObjectResult(_billService.GetDetail(id));
        }
        [HttpPost]
        public IActionResult SaveEntity(BillViewModel billVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (billVm.Id == 0)
            {
                _billService.Create(billVm);
            }
            else
            {
                _billService.Update(billVm);
            }
            _billService.Save();
            return new OkObjectResult(billVm);
        }
    }
}
