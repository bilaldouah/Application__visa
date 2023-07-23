using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class AuthentificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            if(user.login!= null && user.pwd !=null ) 
            {
                if (user.loginUser().id != null)
                {


                    HttpContext.Session.SetInt32("userId", user.loginUser().id.Value);

                    HttpContext.Session.SetString("userLogin", user.loginUser().login);
                    ViewBag.login = HttpContext.Session.GetString("userLogin");

                    HttpContext.Session.SetString("userRole", user.loginUser().role.nom);
                    if (user.loginUser().role.nom == "Admin")
                    {
                        var routeValues = new RouteValueDictionary();
                        routeValues.Add("serviceId", -1);
                        routeValues.Add("date", "");
                        return RedirectToAction("dashboardTable", "Statistique", routeValues);
                    }
                    return RedirectToAction("Index", "Aposstille");


                }
            }
            if (user.login == null)
            {
                ViewBag.loginError = "Login est obligatoire";
            }
            if (user.pwd == null)
            {
                ViewBag.pwdError = "mot de passe est obligatoire";
            }
            if(user.loginUser().id == null  && user.pwd != null)
            {
                ViewBag.wrongData = "incorrect mot de passe ou  Login, Veuillez essayer une autre fois";
            }

            return View();
        }

        public IActionResult Modifier_MotDePasse()
        {
            return View();
        }

    }
}
