using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]      //bo dieu khien API
    [Route("api/[controller]")]     // root cho bo dieu khien API
    public class BaseApiController : ControllerBase   // ap dung MVC cho controllerbase (model - view - controller)
    {
    }
}
