using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AileLeve.Controllers
{

    public class ProfilController : Controller
    {

        private Dal dal = new Dal();

        public IActionResult Modifier(int id)
        {

            if (id != 0)
            {
                CompteViewModel viewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
                if (viewModel.Authentifie)
                {
                    viewModel.Compte = dal.ObtenirCompte(HttpContext.User.Identity.Name);
                    UtilisateurCompletViewModel utilisateurCompletViewModel = new UtilisateurCompletViewModel();
                    utilisateurCompletViewModel.Compte = dal.ObtenirCompte(id);
                    utilisateurCompletViewModel.Profil = dal.ObtenirProfil(id);
                    utilisateurCompletViewModel.Utilisateur = dal.ObtenirUtilisateur(id);

                    return View(utilisateurCompletViewModel);
                }
                return Redirect("/Utilisateur/Connexion");
            }
            return View("Error");
        }



        [HttpPost]
        public IActionResult Modifier(UtilisateurCompletViewModel utilisateurAmodifier)
        {

            CompteViewModel viewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };


            if (viewModel.Authentifie)
            {
                dal.ModifierUtilisateur(utilisateurAmodifier.Utilisateur);
                dal.ModifierCompte(utilisateurAmodifier.Compte);
                dal.ModifierProfil(utilisateurAmodifier.Profil);


                return RedirectToAction("Index", "Home", new { @id = utilisateurAmodifier.Profil.Id });

            }
            else
            {
                return Redirect("/Utilisateur/Connexion");
            }
        }

    }
}