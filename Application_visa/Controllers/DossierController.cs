using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class DossierController : Controller
    {
        public IActionResult Index()
        {
            ViewData["apostille"] = aposstille.getAll((int)HttpContext.Session.GetInt32("userId"));
            return View();
        }
    }
}
