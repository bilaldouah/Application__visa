using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application_visa.Controllers
{
    public class EducationController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.fourn = Fournisseur.getTraduction();
            return View();
        }
        [HttpPost]
        public IActionResult Index(Education edd)
        {
            if (edd.ami_khaled == true)
            {
                edd.prix = edd.charge + edd.charge2 + edd.charge3;
            }

            if (edd.prix >= edd.charge + edd.charge2 + edd.charge3 && edd.total >= edd.charge + edd.charge2 + edd.charge3 && edd.prix <= edd.total && edd.file3 != null)
            {

                edd.scan = file(edd.file3);
                if (edd.file != null)
                {
                    edd.scan1 = file(edd.file);
                }
                if (edd.file1 != null)
                {
                    edd.scan2 = file(edd.file1);
                }
                if (edd.file2 != null)
                {
                    edd.scan3 = file(edd.file2);
                }
                edd.add();

            }
            else
            {
                ViewBag.fourn = Fournisseur.getTraduction();

                ViewData["erorPrix"] = "le prix et le total doit étre plus ou égale les charges et le document de traduction et obligatoire ";
                return View();

            }

            return RedirectToAction("index", "Home");

        }

        public IActionResult Modifier(int id)
        {

            return View(Education.getEducation(id));
        }

        [HttpPost]
        public IActionResult Modifier(Education app)
        {
            if (app.prix >= app.charge + app.charge2 + app.charge3 && app.total >= app.charge + app.charge2 + app.charge3)
            {

                if (app.file != null)
                {
                    if (file(app.file) != null)
                    {
                        app.scan1 = file(app.file);
                    }
                    else
                    {
                        return RedirectToAction("Modifier");
                    }
                }
                if (app.file1 != null)
                {
                    if (file(app.file1) != null)
                    {
                        app.scan2 = file(app.file1);
                    }
                    else
                    {
                        return RedirectToAction("Modifier");
                    }
                }
                if (app.file2 != null)
                {
                    if (file(app.file2) != null)
                    {
                        app.scan3 = file(app.file2);
                    }
                    else
                    {
                        return RedirectToAction("Modifier");
                    }
                }
                app.update();
                ViewData["erorPrix"] = "l'Education a etait bien enregistrer";
                return RedirectToAction("index", "Home");
            }

            else
            {
                ViewData["erorPrix"] = "le prix doit etre plus ou egale la charge";

            }
            return RedirectToAction("Modifier");


        }

        public String file(IFormFile file)
        {
            String[] ext = { ".jpg", ".png", ".jpeg", ".pdf" };
            String file_ext = Path.GetExtension(file.FileName).ToLower();


            if (ext.Contains(file_ext))
            {
                String newName = Guid.NewGuid() + file.FileName;
                String path_file = Path.Combine("wwwroot/scans", newName);

                using (FileStream stream = System.IO.File.Create(path_file))
                {
                    file.CopyTo(stream);
                }
                ViewData["erorPrix"] = "l'education a etait bien enregistrer";
                return newName;
            }
            else
            {
                ViewData["erorPrix"] = "le scan de fichier doit etre un PDF ou Image";

            }
            return null;
        }

    }
}