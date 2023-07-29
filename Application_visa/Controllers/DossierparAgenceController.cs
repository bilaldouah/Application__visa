using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class DossierparAgenceController : Controller
    {
        public IActionResult Index()
        {
            /*User user = Models.User.getUsersById_((int)HttpContext.Session.GetInt32("userId"));
            ViewData["assurance"] = Files.getAllbyAgence(user.agence.id);
            ViewData["appostille"] = Files.getAllAppostilbyAgence(user.agence.id);
            ViewData["tourism"] = Tourism.getAllbyAgence(user.agence.id);
            ViewData["Traduction"] = Files.getAllTraductionByAgence(user.agence.id);
            ViewData["Education"] = Files.GetAllEducationByAgence(a.id);*/
            return View();
        }
    }
}
