using Application_visa.filters;
using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text;

namespace Application_visa.Controllers
{
    public class UserController : Controller
    {
        [AuthentificationFilter]
        public IActionResult UpdatePassword()
        {
            return View();
        }
        [AuthentificationFilter]

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

        public IActionResult MotDePasseOublier()
        {
            return View();
                }
        [HttpPost]
        public IActionResult MotDePasseOublier(String email)
        {
            User u = new User();
            if(u.searchUserbyemail(email) == false || email==null)
            {
                ViewBag.eror = "Email n'existe pas";
                return View();
            }
            HttpContext.Session.SetInt32("nbr", EnvoyerNombre(email));
            HttpContext.Session.SetString("email", email);
            return RedirectToAction("NombreDeRecuperation");

        }
        
        public IActionResult NombreDeRecuperation()
        {
            return View();        
        }
        [HttpPost]
        public IActionResult NombreDeRecuperation(int nbr)
        {
            if (nbr == 0)
            {
                ViewBag.Null = "le Nombre est Obligatoire";
                return View();
            }
            int random = (int)HttpContext.Session.GetInt32("nbr"); ;
            if (nbr == random)
            {
                return RedirectToAction("ChangerPassword");

            }
            else
            {
                ViewBag.eror = "Nombre incorrect";
                return View();
            }
        }
        
        public IActionResult ChangerPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangerPassword(string password, string confirmer)
        {
            if (password == null || confirmer == null)
            {
                ViewBag.Null = "Mot de passe est confirmation son obligatoire";
                return View();
            }
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("MotDePasseOublier");
            }
            if (password.Equals(confirmer))
            {
                String email = HttpContext.Session.GetString("email");
                Models.User.updatepwdbymail(email,hashPassword(password));
                return RedirectToAction("Index", "Authentification");
            }
            ViewBag.Passworderor = "Mot de passe et la COnfirmation sont differents";
            return View();
        }
        public int EnvoyerNombre(string mail)
        {
            Random random = new Random();

            int randomNumber = random.Next(1000, 10000);

            string from = "eelvisado@gmail.com";
            //password email eelvisado@gmail.comapkS
            string subject = "Modifier mot de passe ";
            string body = "Nous avons bien reçu votre demande de récupération de mot de passe. Pour procéder à la récupération, veuillez utiliser le code de vérification suivant :\r\n\r\n" + randomNumber + "\r\n\r\nVeuillez noter que ce code de vérification est valable pendant une durée limitée. Une fois que vous avez reçu cet e-mail, veuillez saisir le code dans le champ prévu à cet effet sur notre site Web.\r\n\r\nSi vous n'avez pas initié cette demande de récupération de mot de passe, veuillez ignorer cet e-mail et prendre les mesures appropriées pour sécuriser votre compte.\r\n\r\n| |\r\n| [ CODE :" + randomNumber + " ] |\r\n| |\r\nCordialement,\r\nL'équipe du Elvisado";

            MailMessage message = new MailMessage(from, mail, subject, body);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential("eelvisado@gmail.com", "vlibbddklwljpyld");
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
            return randomNumber;
        }

    }
}
