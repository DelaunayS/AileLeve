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
            Cours cours = dal.ObtenirTousLesCours().Where(c => c.Id == id).FirstOrDefault();
            dal.CreerEtudie(eleve.Id, cours.Id);
            EstDisponible propose = dal.ObtenirToutesLesDispos().Where(c => c.CoursId == cours.Id).FirstOrDefault();
            EmploiDuTempsEnseignant edt = dal.ObtenirTousLesEmploisDuTemps().Where(c => c.Id == propose.EmploiDuTempsEnseignantId).FirstOrDefault();
            dal.NestPlusDisponible(edt.Id);
            return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
        }


        [HttpGet]
        public IActionResult AttribuerCours(int id)
        {
            CompteViewModel cvm = new CompteViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated
            };

            int.TryParse(HttpContext.User.Identity.Name, out int idCompte);
            cvm.Compte = dal.ObtenirTousLesComptes().Where(c => c.Id == idCompte).FirstOrDefault();
            cvm.Enseignant = dal.ObtenirTousLesEnseignants().Where(c => c.UtilisateurId == cvm.Compte.Utilisateur.Id).FirstOrDefault();
            Compte compteAdmin = dal.ObtenirTousLesComptes().Where(c => c.Role == "Admin").FirstOrDefault();
            Enseignant ensAdmin = dal.ObtenirTousLesEnseignants().Where(c => c.UtilisateurId == compteAdmin.Utilisateur.Id).FirstOrDefault();
            if (ensAdmin == null)
            {
                cvm.ListeSimpleCours = new List<Cours>();
                return View(cvm);
            } else
            {
                cvm.ListeSimpleCours = dal.ObtenirTousLesCours().Where(c => c.EnseignantId == ensAdmin.Id).ToList();
                return View(cvm);
            }
        }

        public IActionResult Attribuer(int id)
        {
            int.TryParse(HttpContext.User.Identity.Name, out int idCompte);
            Compte compte = dal.ObtenirCompte(idCompte);
            Cours cours = dal.ObtenirTousLesCours().Where(c => c.Id == id).FirstOrDefault();
            Enseignant enseignant = dal.ObtenirTousLesEnseignants().Where(c => c.UtilisateurId == compte.Utilisateur.Id).FirstOrDefault();
            dal.ModifierEnseignantDuCours(cours.Id, enseignant.Id);
            DateTime date = DateTime.Now;
            int idEdt = dal.CreerEmploiDuTemps(date);
            dal.CreerEstDisponible(enseignant.Id, idEdt, cours.Id);
            return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
        }


        [Authorize]
        [Authorize(Roles = "Eleve, Enseignant,Recrutement")]
        public IActionResult Supprimer(int id)
        {
            CompteViewModel evm = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };

                evm.Compte = dal.ObtenirCompte(HttpContext.User.Identity.Name);
                evm.Eleve = dal.ObtenirTousLesEleves().Where(c => c.UtilisateurId == evm.Compte.UtilisateurId).FirstOrDefault();
                evm.Etudie = dal.ObtenirToutesLesReservationsCours().Where(c => c.CoursId == id).FirstOrDefault();
                evm.EstDisponible = dal.ObtenirTousLesPlannings().Where(c => c.CoursId == evm.Etudie.CoursId).FirstOrDefault();
                dal.SupprimerEtudie(evm.Etudie);             
                dal.EstDisponible(evm.EstDisponible.EmploiDuTempsEnseignant.Id);
                return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
        }


        [Authorize(Roles = "Admin")]
        public IActionResult SupprimerCoursAdmin(int id)
        {
            Cours cours = dal.ObtenirTousLesCours().Where(c => c.Id == id).FirstOrDefault();
            Etudie reservation = dal.ObtenirTousLesEtudie().Where(c => c.CoursId == cours.Id).FirstOrDefault();
            EstDisponible proposition = dal.ObtenirTousLesPlannings().Where(c => c.CoursId == cours.Id).FirstOrDefault();

            dal.SupprimerEtudie(reservation);
            dal.SupprimerEstDisponible(proposition);
            dal.SupprimerCours(cours);

            return RedirectToAction("EmploiDuTempsEnseignant", "Enseignant");
        }


    }
}
