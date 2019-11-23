using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Implementation;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Utilities.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeduCoreApp.Api.Controllers
{
    public class ProductController : ApiController
    {
        IProductService _productService;
      
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            return new OkObjectResult(_productService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            return new OkObjectResult(_productService.GetById(id));
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductViewModel productViewModel)
        {
            productViewModel.SeoAlias = TextHelper.ToUnsignString(productViewModel.Name);
            if (productViewModel.Id == 0)
            {
                _productService.Add(productViewModel);
            }
            else
            {
                _productService.Update(productViewModel);
            }
            _productService.Save();
            return new OkObjectResult(productViewModel);
        }
        [HttpPut]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _productService.Delete(id);
                _productService.Save();

                return new OkObjectResult(id);
            }
        }
    }
}
