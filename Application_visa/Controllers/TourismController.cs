using Application_visa.filters;
using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    [AgentFilter]

    public class TourismController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Tourism torism)
        {
            if (torism.ami_khaled == true)
            {
                torism.prix = torism.charge;
            }
            if (torism.prix >= torism.charge && torism.total >= torism.charge && torism.total >= torism.prix)
            {
                if (torism.file == null)
                {
                    torism.Add();
                    ViewData["erorPrix"] = "Tourism a etait bien enregistrer";
                    return RedirectToAction("Index", "DossierparAgence");
                }
                String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                String file_ext = Path.GetExtension(torism.file.FileName);
                if (ext.Contains(file_ext))
                {
                    String newName = Guid.NewGuid() + torism.file.FileName;
                    String path_file = Path.Combine("wwwroot/scans", newName);
                    torism.scan = newName;
                    torism.Add();
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        torism.file.CopyTo(stream);
                    }
                    ViewData["erorPrix"] = "Tourism a etait bien enregistrer";
                                
                }
            }
            else
            {
                ViewData["erorPrix"] = "le prix doit etre plus ou egale la charge";
            }
            return RedirectToAction("Index", "DossierparAgence");
        }
        public IActionResult Modifier(int id)
        {
            Tourism tourism = new Tourism();
            return View(tourism.getTourism(id)); 
        }
        [HttpPost]
        public IActionResult Modifier( Tourism app)
        {
            if (app.prix >= app.charge && app.total >= app.charge)
            {
                if (app.scan != null)
                {
                    app.Update();
                    ViewData["erorPrix"] = "Tourism a etait bien Modifier";
                    return RedirectToAction("Index", "DossierparAgence");

                }
                if (app.file == null)
                {
                    app.Update();
                    ViewData["erorPrix"] = "Tourism a etait bien enregistrer";
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
                        app.Update();
                        using (FileStream stream = System.IO.File.Create(path_file))
                        {
                            app.file.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "Tourism a etait bien Modifier";
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
