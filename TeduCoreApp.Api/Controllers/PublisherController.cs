using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeduCoreApp.Api.Controllers
{
    public class PublisherController : ApiController
    {
        IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        //GET: api/<controller>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new OkObjectResult(_publisherService.GetById(id));
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_publisherService.GetAll());
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

        [HttpPut]
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
    }
}
