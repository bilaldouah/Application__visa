using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Application_visa.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword(String  old ,String newP ,String Cp) {

            Models.User u = Models.User.getUserPassword((int)HttpContext.Session.GetInt32("userId"));
            if(newP == Cp)
            {
                if(u.pwd == hashPassword(old))
                {
                    u.updatepwd(newP);
                }
                else
                {
                    ViewBag.old = "Mot de passe Incorrect";
                    return View();

                }
            }
            else
            {
                ViewBag.confirm = "Nouveau mot de passe est different de la confirmation";
                return View();
            }
            ViewBag.succes="Mot de passe modifier avec succes";
            return View();

        }
        public string hashPassword(string password)
        {

            if (password != null)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
            return null;
        }

    }
}
