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
            string password = Request.Form["passwordConfirm"];
         
             
                    if (password == user.pwd && user.searchUser(user.login) == false)
                    {
                        user.addUtilisateure();
                    }
                                 
            return RedirectToAction("addUser");
        }


    }
}
