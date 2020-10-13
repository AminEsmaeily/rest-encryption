using Microsoft.AspNetCore.Mvc;
using RESTEncryption.AspNetCoreWebApp.Infrastructure;
using RESTEncryption.AspNetCoreWebApp.Models;

namespace RESTEncryption.AspNetCoreWebApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SampleServiceController : Controller
    {
        [HttpPost]
        [TypeFilter(typeof(RequestBodyDecryptionFilter))]
        public IActionResult SampleAction([FromBody] SampleModel data)
        {
            return Ok("The action invoked successfully.");
        }
    }
}
