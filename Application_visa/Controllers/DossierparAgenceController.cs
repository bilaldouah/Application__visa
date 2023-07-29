using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class DossierparAgenceController : Controller
    {
        public IActionResult Index()
        {
            User user = Models.User.getUsersById_((int)HttpContext.Session.GetInt32("userId"));
            Agence a = Agence.getAgence(user.idagence);
            ViewData["assurance"] = Files.getAllbyAgence(a.id);
            ViewData["appostille"] = Files.getAllAppostilbyAgence();
            ViewData["tourism"] = Tourism.getAllbyAgence();
            ViewData["Traduction"] = Files.getAllTraductionByAgence();
            ViewData["Education"] = Files.GetAllEducationByAgence(a.id);
            return View();
        }
    }
}
