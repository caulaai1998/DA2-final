using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Utilities.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeduCoreApp.Api.Controllers
{
    //[Authorize(Roles = "admin")]
    public class ProductCategoryController : ApiController
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        // GET: api/values
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            return new OkObjectResult(_productCategoryService.GetById(id));
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
                _productCategoryService.Delete(id);
                _productCategoryService.Save();
                return new OkObjectResult(id);
            }
        }

        [HttpPost]
        public IActionResult Add(ProductCategoryViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                productVm.SeoAlias = TextHelper.ToUnsignString(productVm.Name);
                if (productVm.Id == 0)
                {
                    _productCategoryService.Add(productVm);
                }
                else
                {
                    _productCategoryService.Update(productVm);
                }
                _productCategoryService.Save();
                return new OkObjectResult(productVm);

            }
        }

        //[HttpPost]
        //public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new BadRequestObjectResult(ModelState);
        //    }
        //    else
        //    {
        //        if (sourceId == targetId)
        //        {
        //            return new BadRequestResult();
        //        }
        //        else
        //        {
        //            _productCategoryService.UpdateParentId(sourceId, targetId, items);
        //            _productCategoryService.Save();
        //            return new OkResult();
        //        }
        //    }
        //}

        //[HttpPost]
        //public IActionResult ReOrder(int sourceId, int targetId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new BadRequestObjectResult(ModelState);
        //    }
        //    else
        //    {
        //        if (sourceId == targetId)
        //        {
        //            return new BadRequestResult();
        //        }
        //        else
        //        {
        //            _productCategoryService.ReOrder(sourceId, targetId);
        //            _productCategoryService.Save();
        //            return new OkResult();
        //        }
        //    }
        //}
    }
}
