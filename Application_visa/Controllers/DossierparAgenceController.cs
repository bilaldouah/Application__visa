using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class DossierparAgenceController : Controller
    {
        public IActionResult Index()
        {
            User user = Models.User.getUsersById_((int)HttpContext.Session.GetInt32("userId"));
            ViewData["tourism"] = Tourism.getAllbyAgence(); 
            return View();
        }
    }
}
