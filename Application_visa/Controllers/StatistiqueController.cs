using Application_visa.filters;
using Application_visa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace Application_visa.Controllers
{
    [AdminFilter]
    
    public class StatistiqueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult dashboardTable(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {
            ViewBag.idS = serviceId;
            ViewBag.idA = agenceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.login = HttpContext.Session.GetString("userLogin");
            Statistique statistique = new Statistique();
            Service service = new Service();
            Agence agence = new Agence();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                services = service.getAllServices(),
                years = statistique.getAllYears(),
                files = statistique.filterQeury(serviceId, agenceId, dateD, dateF, year,kh),
            };
        
            return View(viewModel);
        }
        public IActionResult detailChart4(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {
            ViewBag.idS = serviceId;
            ViewBag.idA = agenceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.login = HttpContext.Session.GetString("userLogin");
            Statistique statistique = new Statistique();
            Service service = new Service();
            Agence agence = new Agence();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                services = service.getAllServices(),
                years = statistique.getAllYears(),
                files = statistique.getCountFilesDoneList(serviceId, agenceId, dateD, dateF, year, kh),
            };

            return View(viewModel);
        }
        public IActionResult DoneChartAgence(int serviceId, string dateD, string dateF, int year, string kh,int agenceId)
        {
            ViewBag.idS = serviceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.login = HttpContext.Session.GetString("userLogin");
            Statistique statistique = new Statistique();
            Service service = new Service();
            Agence agence = new Agence();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                services = service.getAllServices(),
                years = statistique.getAllYears(),
                files = statistique.getFilesDoneByAg(serviceId, dateD, dateF, year, kh, agenceId),
            };

            return View(viewModel);
        }
        public IActionResult avanceChartAgence(int serviceId, string dateD, string dateF, int year, string kh, int agenceId)
        {
            ViewBag.idS = serviceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.login = HttpContext.Session.GetString("userLogin");
            Statistique statistique = new Statistique();
            Service service = new Service();
            Agence agence = new Agence();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                services = service.getAllServices(),
                years = statistique.getAllYears(),
                files = statistique.getFilesAvanceByAg(serviceId, dateD, dateF, year, kh, agenceId),
            };

            return View(viewModel);
        }
        public IActionResult avanceChartEmp(int serviceId, string dateD, string dateF, int year, string kh, int agenceId, int empId )
        {
            ViewBag.idS = serviceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.login = HttpContext.Session.GetString("userLogin");
            Statistique statistique = new Statistique();
            Service service = new Service();
            Agence agence = new Agence();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                services = service.getAllServices(),
                years = statistique.getAllYears(),
                files = statistique.getFilesAvanceByEmp(serviceId, dateD, dateF, year, kh, agenceId,empId),
            };

            return View(viewModel);
        }
        public IActionResult doneChartEmp(int serviceId, string dateD, string dateF, int year, string kh, int agenceId, int empId)
        {
            ViewBag.idS = serviceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.login = HttpContext.Session.GetString("userLogin");
            Statistique statistique = new Statistique();
            Service service = new Service();
            Agence agence = new Agence();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                services = service.getAllServices(),
                years = statistique.getAllYears(),
                files = statistique.getFilesDoneByEmp(serviceId, dateD, dateF, year, kh, agenceId, empId),
            };

            return View(viewModel);
        }
        public IActionResult chartEmployer(int serviceId, string dateD, string dateF, int year, string kh, int agenceId, int empId)
        {
            ViewBag.idS = serviceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.login = HttpContext.Session.GetString("userLogin");
            Statistique statistique = new Statistique();
            Service service = new Service();
            Agence agence = new Agence();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                services = service.getAllServices(),
                years = statistique.getAllYears(),
                files = statistique.getAllFilesByEmp(serviceId, dateD, dateF, year, kh, agenceId, empId),
            };

            return View(viewModel);
        }
        public IActionResult detailChart3(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {
            ViewBag.idS = serviceId;
            ViewBag.idA = agenceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.login = HttpContext.Session.GetString("userLogin");
            Statistique statistique = new Statistique();
            Service service = new Service();
            Agence agence = new Agence();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                services = service.getAllServices(),
                years = statistique.getAllYears(),
                files = statistique.getFilesAvanceList(serviceId, agenceId, dateD, dateF, year, kh),
            };

            return View(viewModel);
        }
        

        public IActionResult dashboard(int serviceId, int agenceId, string dateD, string dateF, int year, string kh,int agId,int empId)
        {
            ViewBag.idS= serviceId;
            ViewBag.idA = agenceId;
            ViewBag.dateD= dateD;
            ViewBag.dateF= dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            Statistique s = new Statistique();
            Agence agence = new Agence();
            Service service= new Service();
            dateRepository dateRep = new dateRepository();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                countNbrFillesAdded = s.countAddedFiles(serviceId, agenceId, dateD, dateF, year, kh),
                nbrFilesAvance = s.getCountAvance(serviceId, agenceId, dateD, dateF, year, kh),
                nbrFilesDone = s.getCountFilesDone(serviceId, agenceId, dateD, dateF, year, kh),
                services = service.getAllServices(),
                years = s.getAllYears(),
                nbrFilles = s.countFiles(serviceId, agenceId, dateD, dateF, year, kh).Item1,
                monthsNM = s.countFiles(serviceId, agenceId, dateD, dateF, year, kh).Item2,               
            };

            return View(viewModel);
        }
        public IActionResult dashboardAgences(int agId,int serviceId,string dateD, string dateF, int year, string kh)
        {
            ViewBag.idS = serviceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            ViewBag.idAgence = agId;
            Statistique s = new Statistique();
            Agence agence = new Agence();
            Service service = new Service();
            User user= new User();
            dateRepository dateRep = new dateRepository();
            var viewModel = new Statistique
            {
                users = user.getUsersByAgence(agId),
                agences = agence.getAgences(),
                countNbrFillesAdded = s.filterQeuryByAgennce(serviceId,dateD,dateF,year,kh,agId),  
                nbrFilesAvance = s.getCountAvanceByAg(serviceId, dateD, dateF, year, kh, agId),
                nbrFilesDone = s.getCountFilesDoneByAg(serviceId, dateD, dateF, year, kh, agId),
                services = service.getAllServices(),
                years = s.getAllYears(),

                nbrFilles = s.countFilesByAg(serviceId, dateD, dateF, year, kh, agId).Item1,
                 monthsNM = s.countFilesByAg(serviceId, dateD, dateF, year, kh, agId).Item2,
            };

            return View(viewModel);
        }

        public IActionResult dashboardEmp(int empId,int agId, int serviceId, string dateD, string dateF, int year, string kh)
        {
            ViewBag.idS = serviceId;
            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;
            ViewBag.kh = kh;
            Statistique s = new Statistique();
            Agence agence = new Agence();
            Service service = new Service();
            User user = new User();
            dateRepository dateRep = new dateRepository();
            var viewModel = new Statistique
            {
                users = user.getUsersById(empId),
                agences = agence.getAgences(),
                countNbrFillesAdded = s.filterQeuryByEmp(serviceId, dateD, dateF, year, kh, agId,empId),
                nbrFilesAvance = s.getCountAvanceByEmp(serviceId, dateD, dateF, year, kh, agId, empId),
                nbrFilesDone = s.getCountFilesDoneByEmp(serviceId, dateD, dateF, year, kh, agId, empId),
                services = service.getAllServices(),
                years = s.getAllYears(),

                nbrFilles = s.countFilesByEmp(serviceId, dateD, dateF, year, kh, agId, empId).Item1,
                monthsNM = s.countFilesByEmp(serviceId, dateD, dateF, year, kh, agId, empId).Item2,
            };

            return View(viewModel);
        }
        public IActionResult dashboardFournisseur(string dateD, string dateF, int year)
        {

            ViewBag.dateD = dateD;
            ViewBag.dateF = dateF;
            ViewBag.year = year;

            Statistique s = new Statistique();
            dateRepository dateRep = new dateRepository();
            var viewModel = new Statistique
            {

                countSumFourniseurs = s.getCountCharge(dateD, dateF, year),
                fournisseureName = Fournisseur.getAllFournisseurs(),
                years = s.getAllYears(),
            };

            return View(viewModel);
        }
        public IActionResult dashboardSalaire(int month, int year, int agenceId)
        {
            ViewBag.idA = agenceId;
            ViewBag.dateMonth = month;
            ViewBag.year = year;
            Agence agence = new Agence();
            Statistique test = new Statistique();
            Statistique s = new Statistique();
            var viewModel = new Statistique
            {
                agences = agence.getAgences(),
                listChiffreAffaireAndNet = s.getCountSalaireNetAndChiffreAffaire(month, year, agenceId),
                objetTotal = s.getCountTotal(month, year,agenceId),
                years = s.getAllYears(),
            };
            return View(viewModel);
        }
    }
   
}
