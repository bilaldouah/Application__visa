using Application_visa.filters;
using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    [AuthentificationFilter]
    public class AssuranceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(assurance ass)
        {
            if (ass.ami_khaled == true) 
            {
                ass.prix = ass.charge;
            }
            if (ass.prix >= ass.charge && ass.total >= ass.charge && ass.total>=ass.prix)
            {
                int id = (int)HttpContext.Session.GetInt32("userId");
                ass.user = Models.User.getUser(id);
                if (ass.file == null)
                {
                    ass.add();
                    ViewData["erorPrix"] = "Assurance a etait bien enregistrer";
                    return RedirectToAction("index", "Home");

                }
                String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                String file_ext = Path.GetExtension(ass.file.FileName);
                if (ext.Contains(file_ext))
                {
                    String newName = Guid.NewGuid() + ass.file.FileName;
                    String path_file = Path.Combine("wwwroot/scans", newName);
                    ass.scan = newName;
                    ass.add();
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        ass.file.CopyTo(stream);
                    }
                    ViewData["erorPrix"] = "Assurance a etait bien enregistrer";
                }
            }
            else
            {
                ViewData["erorPrix"] = "le prix doit etre plus ou egale la charge";
            }
            return View();
        }
        public IActionResult modifier(int id)
        {
            assurance assurance = new assurance();
            return View(assurance.getAssurance(id));
        }
        [HttpPost]
        public IActionResult modifier(assurance app)
        {
            if (app.prix >= app.charge && app.total >= app.charge)
            {
                if (app.scan != null)
                {
                    app.modifier();
                    ViewData["erorPrix"] = "l'assurance a etait bien Modifier";
                    return RedirectToAction("Index", "DossierparAgence");

                }
                if (app.file == null)
                {
                    app.modifier();
                    ViewData["erorPrix"] = "l'assurance a etait bien enregistrer";
                    return RedirectToAction("Index", "DossierparAgence");

                }
                else
                {
                    String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                    String file_ext = Path.GetExtension(app.file.FileName);
                    if (ext.Contains(file_ext))
                    {
                        String newName = Guid.NewGuid() + app.file.FileName;
                        String path_file = Path.Combine("wwwroot/scans", newName);
                        app.scan = newName;
                        app.modifier();
                        using (FileStream stream = System.IO.File.Create(path_file))
                        {
                            app.file.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "l'assurance a etait bien Modifier";
                    }
                    else
                    {
                        ViewData["erorPrix"] = "le scan de fichier doit etre un PDF ou Image";
                    }
                }
            }
            else
            {
                ViewData["erorPrix"] = "le prix doit etre plus ou egale la charge";

            }
            return RedirectToAction("Index", "DossierparAgence");

        }

    }
}
