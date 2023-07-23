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
            return View();
        }
        [HttpPost]
        public IActionResult Employe(Virment virment)
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
                    User user = Models.User.getUser((int)HttpContext.Session.GetInt32("userId"));
                    ViewData["Users"] = user.getUsersByAgence(user.agence.id);
                    return View();
                }
            }
            virment.VirmentToUser((int)HttpContext.Session.GetInt32("userId"));
            return RedirectToAction("ListeEmploye");
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
            return RedirectToAction("ListeFournisseur");
        }

        public IActionResult Agence()
        {
            ViewData["agence"] = new Agence().getAgences();
            return View();
        }
        [HttpPost]
        public IActionResult Agence(Virment virment)
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
                    ViewData["agence"] = new Agence().getAgences();

                    return View();
                }
            }
            virment.VirmentToAgence((int)HttpContext.Session.GetInt32("userId"));
            return RedirectToAction("ListeAgence");
        }

        public IActionResult ListeFournisseur()
        {
            return View(Virment.getAllFourn((int)HttpContext.Session.GetInt32("userId")));
        }

        public IActionResult ListeEmploye()
        {
            return View(Virment.getAllEmploye((int)HttpContext.Session.GetInt32("userId")));
        }

        public IActionResult ListeAgence()
        {
            return View(Virment.getAllAgence((int)HttpContext.Session.GetInt32("userId")));
        }

        public IActionResult Recu()
        {
            User user = Models.User.getUser((int)HttpContext.Session.GetInt32("userId"));
            return View(Virment.getVirments(user.agence.id));
        }

        public IActionResult Accepter(int id)
        {
            Virment.Accepter(id);
            return RedirectToAction("Recu");
        }

        public IActionResult Modifer_Agence(int id)
        {
           
            return View(Virment.getVirment(id));
        }
        [HttpPost]
        public IActionResult Modifer_Agence(Virment virment)
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
                    Virment.Modifier(virment);
                    return RedirectToAction("ListeAgence");

                }
                else
                {
                    ViewData["eror"] = "le scan de fichier doit etre un PDF ou Image";
                    ViewData["agence"] = new Agence().getAgences();

                    return View();
                }

            }
            else
            {
                ViewData["eror"] = "le scan de fichier est vide";
                return View();
            }

        }

        public IActionResult Modifer_Fournisseur(int id)
        {

            return View(Virment.getVirment(id));
        }
        [HttpPost]
        public IActionResult Modifer_Fournisseur(Virment virment)
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
                    Virment.Modifier(virment);
                    return RedirectToAction("ListeFournisseur");

                }
                else
                {
                    ViewData["eror"] = "le scan de fichier doit etre un PDF ou Image";
                    ViewData["agence"] = new Agence().getAgences();

                    return View();
                }

            }
            else
            {
                ViewData["eror"] = "le scan de fichier est vide";
                return View();
            }

        }

        public IActionResult Modifer_Employe(int id)
        {

            return View(Virment.getVirment(id));
        }
        [HttpPost]
        public IActionResult Modifer_Employe(Virment virment)
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
                    Virment.Modifier(virment);
                    return RedirectToAction("ListeEmploye");

                }
                else
                {
                    ViewData["eror"] = "le scan de fichier doit etre un PDF ou Image";
                    ViewData["agence"] = new Agence().getAgences();

                    return View();
                }

            }
            else
            {
                ViewData["eror"] = "le scan de fichier est vide";
                return View();
            }

        }
    }
}
