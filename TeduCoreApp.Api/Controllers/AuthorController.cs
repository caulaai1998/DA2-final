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
    public class AuthorController : ApiController
    {
        IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        // GET: api/<controller>


        //Get all
        [HttpGet]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_authorService.GetAll());
        }

        // Get theo id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new OkObjectResult(_authorService.GetById(id));
        }
       
        [HttpPost]
        public IActionResult SaveEntity(AuthorViewModel authorVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (authorVm.Id == 0)
                {
                    _authorService.Add(authorVm);
                }
                else
                {
                    _authorService.Update(authorVm);
                }
                _authorService.Save();
                return new OkObjectResult(authorVm);
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
                _authorService.Delete(id);
                _authorService.Save();
                return new OkObjectResult(id);
            }
        }
    }
}
