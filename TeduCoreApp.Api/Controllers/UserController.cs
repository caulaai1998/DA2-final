using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeduCoreApp.Api.Controllers
{
    //[Authorize(Roles = "admin")]
    public class UserController : ApiController
    {

        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    return new OkObjectResult(_userService.GetAll());
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_userService.GetAllAsync());
        }
    }
}

