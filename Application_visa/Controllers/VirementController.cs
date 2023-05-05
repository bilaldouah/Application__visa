using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class VirementController : Controller
    {
        //-------------employe
        public IActionResult Employe()
        {
            User user= Models.User.getUser((int)HttpContext.Session.GetInt32("userId"));
            ViewData["Users"] = user.getUsersByAgence(user.agence.id);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Employe(Virment virment)
        {
            virment.VirmentToUser((int)HttpContext.Session.GetInt32("userId"));
            return View();
        }

        //-------------Fournisseur

        public IActionResult Fournisseur()
        {
            ViewData["Fournisseurs"] = Models.Fournisseur.getAllFournisseurs();
            return View();
        }

        [HttpPost]
        public IActionResult Fournisseur(Virment virment)
        {
            if (virment.file != null)
            {
                String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                String file_ext = Path.GetExtension(virment.file.FileName).ToLower();
                if (ext.Contains(file_ext))
                {
                    String newName = Guid.NewGuid() + virment.file.FileName;
                    String path_file = Path.Combine("wwwroot/scans", newName);
                    virment.scan = newName;
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        virment.file.CopyTo(stream);
                    }
                }
                else
                {
                    ViewData["eror"] = "le scan de fichier doit etre un PDF ou Image";
                    ViewData["Fournisseurs"] = Models.Fournisseur.getAllFournisseurs();

                    return View();
                }
            }
            virment.VirmentToFourn((int)HttpContext.Session.GetInt32("userId"));
            return View();
        }
    }
}
