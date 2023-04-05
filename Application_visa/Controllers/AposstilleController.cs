using Application_visa.Models;

using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class AposstilleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(aposstille app)
        {
            if(app.ami_khaled == true)
            {
                app.prix = app.charge;
            }
                
            if (app.prix >= app.charge && app.total>=app.charge && app.prix <= app.total)
                {
                     int id = (int)HttpContext.Session.GetInt32("userId");
                    app.user=Models.User.getUser(id);
                    if(app.file == null)
                    {
                        app.add();
                        ViewData["erorPrix"] = "l'Aposstille a etait bien enregistrer";
                        return RedirectToAction("index", "Home");

                    }
                    String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                    String file_ext = Path.GetExtension(app.file.FileName).ToLower();
                    if (ext.Contains(file_ext))
                    {
                        String newName = Guid.NewGuid() + app.file.FileName;
                        String path_file=Path.Combine("wwwroot/scans", newName);
                        app.scan= newName;
                        app.add();
                        using(FileStream stream =System.IO.File.Create(path_file))
                        {
                            app.file.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "l'Aposstille a etait bien enregistrer";
                    }
                    else
                    {
                        ViewData["erorPrix"] = "le scan de fichier doit etre un PDF ou Image";
                        return View();
                     }
             }
                else
                {
                    ViewData["erorPrix"]="le prix et le total doit étre plus ou égale la charge ";
                    return View();
                    
                }
            
            return RedirectToAction("index","Home");

        }

        public IActionResult Modifier(int id)
        {
            
            return View(aposstille.getAposstile(id));
        }

        [HttpPost]
        public IActionResult Modifier(aposstille app)
        {
            if (app.prix >= app.charge && app.total >= app.charge)
            {
                if (app.scan != null)
                {
                    app.update();
                    ViewData["erorPrix"] = "l'Aposstille a etait bien Modifier";
                    return RedirectToAction("index", "Home");

                }
                if (app.file == null)
                {
                    app.update();
                    ViewData["erorPrix"] = "l'Aposstille a etait bien enregistrer";
                    return RedirectToAction("index", "Home");

                }
                else
                {
                    String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                    String file_ext = Path.GetExtension(app.file.FileName).ToLower();
                    if (ext.Contains(file_ext))
                    {
                        String newName = Guid.NewGuid() + app.file.FileName;
                        String path_file = Path.Combine("wwwroot/scans", newName);
                        app.scan = newName;
                        app.update();
                        using (FileStream stream = System.IO.File.Create(path_file))
                        {
                            app.file.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "l'Aposstille a etait bien Modifier";
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
            return RedirectToAction("index", "Home");

        }

    }
}
