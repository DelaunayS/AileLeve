using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Controllers
{
    [Authorize(Roles = "Admin, Enseignant, Eleve")]     
    public class EnseignantController : Controller
    {
        private Dal dal = new Dal();

        [HttpGet]
        public IActionResult EmploiDuTempsEnseignant(int id)
        {
            CompteViewModel evm = new CompteViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated,
                Compte = dal.ObtenirCompte(HttpContext.User.Identity.Name),
                ToutesLesPropositionsDeCours = dal.ObtenirTousLesPlannings(),
            };

            evm.Cours = dal.ObtenirTousLesCours().Where(c => c.Id == id).FirstOrDefault();
            evm.Eleve = dal.ObtenirTousLesEleves().Where(c => c.UtilisateurId == evm.Compte.UtilisateurId).FirstOrDefault();     
            return View(evm);
        }

     
        public IActionResult ReserverCours(int id)
        {
            int.TryParse(HttpContext.User.Identity.Name, out int idUser);
            Compte compte = dal.ObtenirCompte(idUser);
            Eleve eleve = dal.ObtenirTousLesEleves().Where(c => c.UtilisateurId == compte.Utilisateur.Id).FirstOrDefault();

            dal.CreerEtudie(eleve.Id, id);
            Cours cours = dal.ObtenirTousLesCours().Where(c => c.Id == id).FirstOrDefault();
            EstDisponible propose = dal.ObtenirToutesLesDispos().Where(c => c.CoursId == cours.Id).FirstOrDefault();
            EmploiDuTempsEnseignant edt = dal.ObtenirTousLesEmploisDuTemps().Where(c => c.Id == propose.EmploiDuTempsEnseignantId).FirstOrDefault();
            dal.NestPlusDisponible(edt.Id);
            return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
        }


       
        [Authorize]
        [Authorize(Roles = "Eleve, Enseignant,Recrutement")]
        public IActionResult Supprimer(int id)
        {
            CompteViewModel evm = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };

            if (evm.Authentifie)
            {
                evm.Compte = dal.ObtenirCompte(HttpContext.User.Identity.Name);
                evm.Eleve = dal.ObtenirTousLesEleves().Where(c => c.UtilisateurId == evm.Compte.UtilisateurId).FirstOrDefault();
                evm.Etudie = dal.ObtenirToutesLesReservationsCours().Where(c => c.CoursId == id).FirstOrDefault();
                evm.EstDisponible = dal.ObtenirTousLesPlannings().Where(c => c.CoursId == evm.Etudie.CoursId).FirstOrDefault();
                dal.SupprimerEtudie(evm.Etudie);             
                dal.EstDisponible(evm.EstDisponible.EmploiDuTempsEnseignant.Id);
            };
                return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
        }


    }
}
