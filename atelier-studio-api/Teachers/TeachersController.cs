using Microsoft.AspNetCore.Mvc;

namespace atelier_studio_api.Teachers;

public class TeachersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}