using Application_visa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application_visa.filters;
namespace Application_visa.Controllers
{
    [AdminFilter]
 
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
                if (user.searchUser(user.login) == true)
                {
                    ViewBag.sameLoginError = "ce login est déja utilisé, veuillez choisir un autre login";
                }
                if (user.searchUserbyemail(user.email) == true)
                {
                    ViewBag.sameemailError = "ce email est déja utilisé, veuillez choisir un autre email";
                }
                if (user.passwordConfirm == user.pwd && user.searchUser(user.login) == false && user.searchUserbyemail(user.email) == false)
                {
                    user.addUtilisateure();
                    ViewBag.successAddition = "votre opération s'étant déroulée avec succès";                 
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
            if (user.email == null)
            {
                ViewBag.emailError = "l'email est obligatoire";
            }


            return View(viewModel);

        }

    }
}
