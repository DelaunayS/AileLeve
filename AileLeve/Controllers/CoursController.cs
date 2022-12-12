using AileLeve.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using AileLeve.ViewModels;


namespace AileLeve.Controllers
{
    public class CoursController : Controller
    {


        Dal dal = new Dal();
      
        public IActionResult Ajouter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ajouter(TypeCours typeCours, string matiere, string niveau, string enseignant)
        {
            CompteViewModel viewModel = new CompteViewModel { 
                Authentifie = HttpContext.User.Identity.IsAuthenticated };

            //string idUserStr = HttpContext.User.Identity.Name;
            //int.TryParse(idUserStr, out int idUser);

            dal.CreerCours(typeCours, matiere, niveau, enseignant);
            return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
