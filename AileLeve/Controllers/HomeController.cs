using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace AileLeve.Controllers
{

    public class HomeController : Controller
    {
        private Dal dal = new Dal();
        [Authorize]
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                return Redirect("/Utilisateur/Connexion");
            }

            CompteViewModel viewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentifie)
            {
                viewModel.Compte = dal.ObtenirCompte(HttpContext.User.Identity.Name);
                viewModel.Utilisateur = dal.ObtenirTousLesUtilisateurs().Where(e => e.Id == viewModel.Compte.Id).FirstOrDefault();
                viewModel.Profil = dal.ObtenirTousLesProfils().Where(e => e.Id == viewModel.Compte.ProfilId).FirstOrDefault();

                if (HttpContext.User.IsInRole("Admin"))
                {
                    viewModel.CoursListe = dal.AdminObtenirCoursProposesAvecDateEtEleve();
                }

                if (HttpContext.User.IsInRole("Enseignant")) 
                    {
                    viewModel.Enseignant = dal.ObtenirTousLesEnseignants().Where(e => e.UtilisateurId == viewModel.Compte.UtilisateurId).FirstOrDefault();
                    viewModel.EstDisponible = dal.ObtenirToutesLesDispos().Where(e => e.EnseignantId == viewModel.Enseignant.Id).FirstOrDefault();
                    viewModel.EmpEns = dal.ObtenirTousLesEmploisDuTemps().Where(e => e.Id == viewModel.EstDisponible.EmploiDuTempsEnseignantId).FirstOrDefault();
                    viewModel.CoursListe = dal.ObtenirCoursProposesAvecDateEtEleve(viewModel.Enseignant.Id);
                };

                if (HttpContext.User.IsInRole("Eleve"))
                {
                    viewModel.Eleve = dal.ObtenirTousLesEleves().Where(e => e.UtilisateurId == viewModel.Compte.UtilisateurId).FirstOrDefault();
                    viewModel.CoursListeEleve = dal.ObtenirCoursReservesAvecDateEtProf(viewModel.Eleve.Id);
                };

                return View(viewModel);                
            }
            return Redirect("/Utilisateur/Connexion");
        }
    }
}