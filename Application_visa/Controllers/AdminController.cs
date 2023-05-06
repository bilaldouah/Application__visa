using Application_visa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult addUser()
        {
         
            Role role = new Role();
            Agence agence = new Agence();        
            var viewModel = new User
            {
                roles = role.getRoles(),
                agences = agence.getAgences()
            };
           return View(viewModel);
      
        }
        [HttpPost]
        public IActionResult addUser(User user)
        {
            Role role = new Role();
            Agence agence = new Agence();
            var viewModel = new User
            {
                roles = role.getRoles(),
                agences = agence.getAgences()
            };
        
            if (user.login!=null && user.pwd!=null && user.passwordConfirm!=null && user.role.id!=null && user.agence.id!=null) 
            {

                if (user.passwordConfirm == user.pwd && user.searchUser(user.login) == false)
                {
                    user.addUtilisateure();
                    return RedirectToAction("addUser");
                }
               
                if (user.passwordConfirm != user.pwd)
                {
                    ViewBag.passwordNotMatchError = "le mot de passe ne correspond pas";
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
                 if (user.passwordConfirm == null)
                {
                    ViewBag.pwdConfirmationError = "mot de pass de confirmation est obligatoire";
                }
                 if (user.role.id == null)
                {
                    ViewBag.roleError = "le role est obligatoire";
                }
                 if (user.agence.id == null)
                {
                    ViewBag.agenceError = "l'agence est obligatoire";
                }
                   

            return View(viewModel);

        }

    }
}
