using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AileLeve.Controllers
{
   
    public class HomeController : Controller
    {    
        private Dal dal = new Dal();   

        public IActionResult Index()
        {
            CompteViewModel viewModel = new CompteViewModel{ Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentifie)
            {
                //Compte monCompte = dal.ObtenirCompte(HttpContext.User.Identity.Name);
                viewModel.Compte = dal.ObtenirCompte(HttpContext.User.Identity.Name);

                UtilisateurCompletViewModel utilisateurCompletViewModel = new UtilisateurCompletViewModel();
                //utilisateurCompletViewModel.Compte = monCompte;
                //utilisateurCompletViewModel.Profil = monCompte.Profil;
                //utilisateurCompletViewModel.Utilisateur = monCompte.Utilisateur;
                utilisateurCompletViewModel.Compte = dal.ObtenirTousLesComptes().Where(p => p.Id == viewModel.Compte.ProfilId).FirstOrDefault();
                utilisateurCompletViewModel.Profil = dal.ObtenirTousLesProfils().Where(p => p.Id == viewModel.Compte.ProfilId).FirstOrDefault();
                utilisateurCompletViewModel.Utilisateur = dal.ObtenirTousLesUtilisateurs().Where(p => p.Id == viewModel.Compte.ProfilId).FirstOrDefault();

                return View(utilisateurCompletViewModel);

            }
            return Redirect("/Utilisateur/Connexion");
        }
       
    }
}