using System.Security.Claims;
using AlWaddahClinic.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace AlWaddahClinic.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly ClinicDbContext _context;

        public BaseController(ClinicDbContext context)
        {
            _context = context;
        }

        protected void AssignAdminstrativeProperties<T>(T obj) where T : Base
        {
            obj.CreatedByUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            obj.CreatedOn = DateTime.Now;
        }
    }
}