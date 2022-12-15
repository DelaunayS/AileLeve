using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace AileLeve.Controllers
{
    [Authorize]     
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
                    utilisateurCompletViewModel.Adresse = dal.ObtenirAdresse(id);
                    return View(utilisateurCompletViewModel);
                }
                return Redirect("/Utilisateur/Connexion");
            }
            return View("Error");
        }
        
        [HttpPost]
        public IActionResult Modifier(UtilisateurCompletViewModel utilisateurAmodifier, int numeroRue, string rue, int codePostal, string ville)
        {
            CompteViewModel viewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentifie)
            {
                dal.ModifierUtilisateur(utilisateurAmodifier.Utilisateur);
                dal.ModifierCompte(utilisateurAmodifier.Compte);
                dal.ModifierProfil(utilisateurAmodifier.Profil);
                if (utilisateurAmodifier.Adresse == null)
                {
                    Adresse adresse = new Adresse
                    {
                        NumeroRue = numeroRue,
                        CodePostal = codePostal,
                        Rue = rue,
                        Ville = ville
                    };
                    dal.AjouterAdresse(utilisateurAmodifier.Compte.Id, adresse);
                }
                else
                {
                    dal.ModifierAdresse(utilisateurAmodifier.Adresse);
                }

                if (HttpContext.User.IsInRole("Eleve"))
                {
                    dal.ModifierDateNaissance(utilisateurAmodifier.Eleve.Id, utilisateurAmodifier.Eleve.DateDeNaissance.ToShortDateString());
                };

                return RedirectToAction("Index", "Home", new { @id = HttpContext.User.Identity.Name });
            }
            else
            {
                return Redirect("/Utilisateur/Connexion");
            }
        }
    }
}
