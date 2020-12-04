using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
       public IActionResult GetRoot()
       {
           var response = new
           {
               href = Url.Link(nameof(GetRoot),null)
           };
           return Ok(response);
       }
    }
}
