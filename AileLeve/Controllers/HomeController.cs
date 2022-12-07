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
                viewModel.Compte = dal.ObtenirCompte(HttpContext.User.Identity.Name);
                UtilisateurCompletViewModel utilisateurCompletViewModel = new UtilisateurCompletViewModel();
                utilisateurCompletViewModel.Compte = dal.ObtenirTousLesComptes().Where(a => a.Id == viewModel.Compte.ProfilId).FirstOrDefault();
                utilisateurCompletViewModel.Profil = dal.ObtenirTousLesProfils().Where(a => a.Id == viewModel.Compte.ProfilId).FirstOrDefault();
                utilisateurCompletViewModel.Utilisateur = dal.ObtenirTousLesUtilisateurs().Where(a => a.Id == viewModel.Compte.ProfilId).FirstOrDefault();

                return View(utilisateurCompletViewModel);
            }
            return Redirect("/Utilisateur/Connexion");
        }
       
    }
}