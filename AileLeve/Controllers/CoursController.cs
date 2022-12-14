using AileLeve.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using AileLeve.ViewModels;
using System.Linq;
using System.Data;

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
        public IActionResult Ajouter(TypeCours typeCours, string matiere, string niveau)
        {
            CompteViewModel viewModel = new CompteViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated
            };

            string idUserStr = HttpContext.User.Identity.Name;
            int.TryParse(idUserStr, out int idUser);
            Enseignant enseignant = dal.ObtenirTousLesEnseignants().Where(p => p.Id == idUser).FirstOrDefault();

            dal.CreerCours(typeCours, matiere, niveau, enseignant.Id);
           
            DateTime date = DateTime.Now;
            dal.CreerNotification("Un nouveau cours de " + matiere + " de niveau " + niveau + " et de type " + typeCours 
            + " a été créé le " + date.ToString("MM/dd/yyyy f HH:mm"));
           
                return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Supprimer(int id)
        {
            CoursViewModel cvm = new CoursViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated
                 
            };
            string idUserStr = HttpContext.User.Identity.Name;
            int.TryParse(idUserStr, out int idUser);
            Enseignant enseignant = dal.ObtenirTousLesEnseignants().Where(p => p.Id == idUser).FirstOrDefault();
            cvm.CoursListe = dal.ObtenirCoursParEnseignant(enseignant.Id);

            return View(cvm);

        }


        public IActionResult SupprimerCours (int id, CoursViewModel coursASupprimer, TypeCours typeCours, string matiere, string niveau, string enseignant)
        {
            CoursViewModel viewModel = new CoursViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated
            };
            if (viewModel.Authentifie)
            {

                viewModel.Cours = dal.ObtenirCours(id);
                 Cours cours = dal.ObtenirTousLesCours().Where(p => p.Id == viewModel.Cours.Id).FirstOrDefault();
                string idUserStr = HttpContext.User.Identity.Name;
                int.TryParse(idUserStr, out int idUser);
               

                dal.SupprimerCours(cours);

                
                return RedirectToAction("Supprimer", "Cours", new {@id = idUser});
            }
            return View();

        }

        public IActionResult ListeCours()
        {
            return View();
        }


    }

}

   