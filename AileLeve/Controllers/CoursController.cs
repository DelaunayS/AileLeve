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
using Microsoft.AspNetCore.Http;
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
            int coursId = dal.CreerCours(typeCours, matiere, niveau, enseignant.Id);
            dal.CreerEstDisponible(enseignant.Id, emploiDuTempsId, coursId);
            
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

            if (HttpContext.User.IsInRole ("Admin"))
            {
            string idUserStr = HttpContext.User.Identity.Name;
             int.TryParse(idUserStr, out int idUser);
            Enseignant enseignant = dal.ObtenirTousLesEnseignants().Where(p => p.Id == idUser).FirstOrDefault();
            cvm.CoursListe = dal.ObtenirCoursParEnseignantPourAdmin();
            } else
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
                return RedirectToAction("Index", "Home", new { @id = idUser });
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

            cvm.Cours = dal.ObtenirCours(id);
            cvm.EstDisponible = dal.ObtenirTousLesPlannings().Where(c => c.CoursId == cvm.Cours.Id).FirstOrDefault();        
            return View(cvm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Enseignant")]    
        public IActionResult Modifier(CoursViewModel coursAModifier, DateTime creneau)
        {
            CoursViewModel viewModel = new CoursViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated
            };
            if (viewModel.Authentifie)
            {
                string idUserStr = HttpContext.User.Identity.Name;
                int.TryParse(idUserStr, out int idUser);

                coursAModifier.EstDisponible = dal.ObtenirToutesLesDispos().Where(c => c.CoursId == coursAModifier.Cours.Id).FirstOrDefault();
                EmploiDuTempsEnseignant edtAmodifier = dal.ObtenirTousLesEmploisDuTemps().Where(c => c.Id == coursAModifier.EstDisponible.EmploiDuTempsEnseignantId).FirstOrDefault();
                edtAmodifier.DateTime = creneau;

                dal.ModifierEmploiDuTemps(edtAmodifier);
                dal.ModifierCours(coursAModifier.Cours);


                return Redirect("/Home/Index");
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

