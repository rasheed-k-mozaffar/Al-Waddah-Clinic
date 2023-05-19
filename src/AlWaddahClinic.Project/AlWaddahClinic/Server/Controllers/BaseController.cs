using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace AlWaddahClinic.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected void AssignAdminstrativeProperties<T>(T obj) where T : Base
        {
            obj.CreatedByUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            obj.CreatedOn = DateTime.Now;
        }
    }
}