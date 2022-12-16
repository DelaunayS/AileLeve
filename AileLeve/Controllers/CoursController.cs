using AileLeve.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using AileLeve.ViewModels;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace AileLeve.Controllers
{
    public class CoursController : Controller
    {

        private Dal dal = new Dal();
        
        [Authorize(Roles = "Admin, Enseignant")]        
        public IActionResult Ajouter()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Enseignant")]       
        public IActionResult Ajouter(TypeCours typeCours, string matiere, string niveau, DateTime creneau)
        {
            CompteViewModel viewModel = new CompteViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated
            };
            int emploiDuTempsId  = dal.CreerEmploiDuTemps(creneau) ;
            string idUserStr = HttpContext.User.Identity.Name;
            int.TryParse(idUserStr, out int idUser);
            Enseignant enseignant = dal.ObtenirTousLesEnseignants().Where(p => p.Id == idUser).FirstOrDefault();
           

            dal.CreerCours(typeCours, matiere, niveau, enseignant.Id);
            dal.CreerEstDisponible(enseignant.Id, emploiDuTempsId);
            
      

            DateTime date = DateTime.Now;
            dal.CreerNotification("Un nouveau cours de " + matiere + " de niveau " + niveau + " et de type " + typeCours
            + " a été créé le " + date.ToString("MM/dd/yyyy f HH:mm"));

            return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Enseignant")]     
        public IActionResult Supprimer(int id, string role)
        {
            CoursViewModel cvm = new CoursViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated

            };


            if (id == 11)
            {
                string idUserStr = HttpContext.User.Identity.Name;
            int.TryParse(idUserStr, out int idUser);

            Enseignant enseignant = dal.ObtenirTousLesEnseignants().Where(p => p.Id == idUser).FirstOrDefault();

            cvm.CoursListe = dal.ObtenirCoursParEnseignantPourAdmin();

            }

            else
           // if (role == "Enseignant")
            {
            string idUserStr = HttpContext.User.Identity.Name;
                int.TryParse(idUserStr, out int idUser);

            Enseignant enseignant = dal.ObtenirTousLesEnseignants().Where(p => p.Id == idUser).FirstOrDefault();

                cvm.CoursListe = dal.ObtenirCoursParEnseignant(enseignant.Id);

            }

            return View(cvm);
        }
       
        [Authorize(Roles = "Admin, Enseignant")]     
        public IActionResult SupprimerCours(int id, CoursViewModel coursASupprimer, TypeCours typeCours, string matiere, string niveau, string enseignant, string utilisateur)
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


                return RedirectToAction("Supprimer", "Cours", new { @id = idUser });
            }
            return View();
        }


       
        [HttpGet]
        [Authorize(Roles = "Admin, Enseignant")]          
        public IActionResult Modifier(int id)
        {
            string idUserStr = HttpContext.User.Identity.Name;
            int.TryParse(idUserStr, out int idUser);

            if (id == 0)
            {
                return RedirectToAction("Supprimer", "Cours", new { @id = idUser });
            }

            CoursViewModel cvm = new CoursViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated

            };

            Enseignant enseignant = dal.ObtenirTousLesEnseignants().Where(p => p.Id == idUser).FirstOrDefault();
            cvm.Cours = dal.ObtenirCours(id);

            return View(cvm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Enseignant")]    
        public IActionResult Modifier(int id, CoursViewModel coursAModifier, TypeCours typeCours, string matiere, string niveau, string enseignant)
        {
            CoursViewModel viewModel = new CoursViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated
            };
            if (viewModel.Authentifie)
            {
                string idUserStr = HttpContext.User.Identity.Name;
                int.TryParse(idUserStr, out int idUser);

                dal.ModifierCours(coursAModifier.Cours);

                return RedirectToAction("Supprimer", "Cours", new { @id = idUser });
            }
            return View();
        }

        [Authorize]
        public IActionResult ListeCours()
        {
            return View();
        }
    }
}

