using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class DossierController : Controller
    {
        public IActionResult Index()
        {
            ViewData["apostille"] = aposstille.getAll((int)HttpContext.Session.GetInt32("userId"));
            ViewData["Education"] = Education.GetAll((int)HttpContext.Session.GetInt32("userId"));
            ViewData["Tourism"] = Tourism.GetAll((int)HttpContext.Session.GetInt32("userId"));
            ViewData["Traduction"] = Traduction.GetAll((int)HttpContext.Session.GetInt32("userId"));
            ViewData["Transaction_Dargent"] = Transaction_Dargent.GetAll((int)HttpContext.Session.GetInt32("userId"));
            ViewData["assurance"] = assurance.GetAll((int)HttpContext.Session.GetInt32("userId"));
            return View();
        }
    }
}
