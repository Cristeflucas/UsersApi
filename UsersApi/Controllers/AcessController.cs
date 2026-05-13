using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessController : ControllerBase
    {

        [HttpGet]
        [Authorize(Policy = "MinimumAge")]
        public IActionResult GetAcess()
        {
            return Ok("Acess granted");
        }
    }
}
