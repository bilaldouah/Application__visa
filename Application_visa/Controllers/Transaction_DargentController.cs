using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class Transaction_DargentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Transaction_Dargent tr)
        {
            if(tr.prix>=tr.charge && tr.total>=tr.charge && tr.total >= tr.prix) 
            {
                if (tr.file == null)
                {
                    tr.Add();
                    ViewData["erorPrix"] = "Transaction d'argent a etait bien enregistrer";
                    return RedirectToAction("index", "Home");

                }
                String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                String file_ext = Path.GetExtension(tr.file.FileName) .ToLower();
                if (ext.Contains(file_ext))
                {
                    String newName = Guid.NewGuid() + tr.file.FileName;
                    String path_file = Path.Combine("wwwroot/scans", newName);
                    tr.scan = newName;
                    tr.Add();
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        tr.file.CopyTo(stream);
                    }
                    ViewData["erorPrix"] = "Transaction a etait bien enregistrer";
                }
            }
            else
            {
                ViewData["erorPrix"] = "le prix doit etre plus ou egale la charge";
            }
            return View();
        }
        public IActionResult Modifier(int id) 
        {
            return View(Transaction_Dargent.getTransaction_Dargent(id));
        }
        [HttpPost]
        public IActionResult Modifier( Transaction_Dargent t)
        {
            if(t.prix>=t.charge && t.total>t.charge && t.total >= t.prix)
            {
                if (t.scan != null)
                {
                    t.Update();
                    ViewData["erorPrix"] = "Transaction a etait bien Modifier";
                    return RedirectToAction("index", "Home");

                }
                if (t.file == null)
                {
                    t.Update();
                    ViewData["erorPrix"] = "Transaction a etait bien enregistrer";
                    return RedirectToAction("index", "Home");

                }
                else
                {
                    String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                    String file_ext = Path.GetExtension(t.file.FileName);
                    if (ext.Contains(file_ext))
                    {
                        String newName = Guid.NewGuid() + t.file.FileName;
                        String path_file = Path.Combine("wwwroot/scans", newName);
                        t.scan = newName;
                        t.Update();
                        using (FileStream stream = System.IO.File.Create(path_file))
                        {
                            t.file.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "Transaction a etait bien Modifier";
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
