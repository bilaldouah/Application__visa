using Application_visa.filters;
using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    [AgentFilter]

    public class Mes_VirementController : Controller
    {
        public IActionResult Index()
        {
            return View(Virment.MesVirement((int)HttpContext.Session.GetInt32("userId")));
        }

        public IActionResult Accepter(int id)
        {
            Virment.Accepter(id);
            return RedirectToAction("Index");
        }
    }
}
