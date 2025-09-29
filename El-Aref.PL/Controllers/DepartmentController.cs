using Microsoft.AspNetCore.Mvc;

namespace El_Aref.PL.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
