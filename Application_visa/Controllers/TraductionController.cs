using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application_visa.Controllers
{
    public class TraductionController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.fourniseur = Fournisseur.getTraduction();
            return View();
        }
        [HttpPost]
        public IActionResult Index(Traduction t)
        {
            if (t.ami_khaled == true)
            {
                t.prix = t.charge;
            }
            if (t.prix >= t.charge && t.total >= t.charge && t.total >= t.prix) 
            {
                if (t.file == null)
                {
                    ViewData["erorPrix"] = "scan avant la traduction et obligatoire.";
                    return RedirectToAction("index", "Home");
                }
                if(t.file !=null && t.file_1 !=null)
                {
                    String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                    String file_ext1 = Path.GetExtension(t.file.FileName).ToLower();
                    String file_ext = Path.GetExtension(t.file_1.FileName).ToLower();
                    if (ext.Contains(file_ext1))
                    {
                        String newName = Guid.NewGuid() + t.file.FileName;
                        String newName1 = Guid.NewGuid() + t.file_1.FileName;
                        String path_file = Path.Combine("wwwroot/scans", newName);
                        String path_file1 = Path.Combine("wwwroot/scans", newName1);
                        t.scan =newName;
                        t.scan1=newName1;
                        t.Add();
                        using (FileStream stream = System.IO.File.Create(path_file))
                        {
                            t.file.CopyTo(stream);
                        }
                        using (FileStream stream = System.IO.File.Create(path_file1))
                        {
                            t.file.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "Traduction a etait bien enregistrer";
                    }
                    else
                    {
                        ViewData["erorPrix"] = "le scan de fichier doit etre un PDF ou Image";
                        return View();
                    }
                }
                if (t.file != null && t.file_1==null)
                {
                    String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                    String file_ext = Path.GetExtension(t.file.FileName).ToLower();
                    if (ext.Contains(file_ext))
                    {
                        String newName = Guid.NewGuid() + t.file.FileName;
                        String path_file = Path.Combine("wwwroot/scans", newName);
                        t.scan = newName;
                        t.Add();
                        using (FileStream stream = System.IO.File.Create(path_file))
                        {
                            t.file.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "Traduction a etait bien enregistrer";
                    }
                    else
                    {
                        ViewData["erorPrix"] = "le scan de fichier doit etre un PDF ou Image";
                        return View();
                    }
                }
                if(t.file_1 != null && t.file==null) 
                {
                    String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                    String file_ext = Path.GetExtension(t.file_1.FileName).ToLower();
                    if (ext.Contains(file_ext))
                    {
                        String newName = Guid.NewGuid() + t.file_1.FileName;
                        String path_file = Path.Combine("wwwroot/scans", newName);
                        t.scan1 = newName;
                        t.Add();
                        using (FileStream stream = System.IO.File.Create(path_file))
                        {
                            t.file_1.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "Traduction a etait bien enregistrer";
                    }
                    else
                    {
                        ViewData["erorPrix"] = "le scan de fichier doit etre un PDF ou Image";
                        return View();
                    }
                }
            }
            else
            {
                ViewData["erorPrix"] = "le prix et le total doit étre plus ou égale la charge ";
                return View();

            }
            return RedirectToAction("index", "Home");
        }
        public IActionResult Modifier(int id)
        {
            return View(Traduction.getTraduction(id));
        }
        [HttpPost]
        public IActionResult Modifier(Traduction t)
        {
           
            if (t.prix >= t.charge && t.total >= t.charge)
            {
                if (t.scan1 != null)
                {
                    t.Update();
                    ViewData["erorPrix"] = "Traduction a etait bien Modifier";
                    return RedirectToAction("index", "Home");
                }
                if (t.file_1 == null)
                {
                    t.Update();
                    ViewData["erorPrix"] = "Traduction a etait bien enregistrer";
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
                    String file_ext = Path.GetExtension(t.file_1.FileName).ToLower();
                    if (ext.Contains(file_ext))
                    {
                        String newName = Guid.NewGuid() + t.file_1.FileName;
                        String path_file = Path.Combine("wwwroot/scans", newName);
                        t.scan1 = newName;
                        t.Update();
                        using (FileStream stream = System.IO.File.Create(path_file))
                        {
                            t.file_1.CopyTo(stream);
                        }
                        ViewData["erorPrix"] = "Traduction a etait bien Modifier";
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
