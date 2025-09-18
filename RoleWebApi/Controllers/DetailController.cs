using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class DetailController : ControllerBase
    {
        [HttpGet]
        public string GetDetails()
        {
            return "hello test ir";
        }
    }
}
