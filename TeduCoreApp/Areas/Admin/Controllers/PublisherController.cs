using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class PublisherController : BaseController
    {
        private IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {

            _publisherService = publisherService;
        }

        public IActionResult Index()
        {
            ViewData["sortOrder"] = _publisherService.GetAll();
            return View();
        }

        #region Get Data API

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _publisherService.GetById(id);

            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(PublisherViewModel publisherVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (publisherVm.Id == 0)
                {
                    _publisherService.Add(publisherVm);
                }
                else
                {
                    _publisherService.Update(publisherVm);
                }
                _publisherService.Save();
                return new OkObjectResult(publisherVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new BadRequestResult();
            }
            else
            {
                _publisherService.Delete(id);
                _publisherService.Save();
                return new OkObjectResult(id);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _publisherService.GetAll();
            return new OkObjectResult(model);
        }
        #endregion Get Data API
    }
}