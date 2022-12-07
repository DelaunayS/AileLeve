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
                    utilisateurCompletViewModel.Compte = dal.ObtenirTousLesComptes().Where(p => p.Id == viewModel.Compte.ProfilId).FirstOrDefault();
                    utilisateurCompletViewModel.Profil = dal.ObtenirTousLesProfils().Where(p => p.Id == viewModel.Compte.ProfilId).FirstOrDefault();
                    utilisateurCompletViewModel.Utilisateur = dal.ObtenirTousLesUtilisateurs().Where(p => p.Id == viewModel.Compte.ProfilId).FirstOrDefault();
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

                //int idCompte;
                //int.TryParse(HttpContext.User.Identity.Name, out int idCompte);
                //Compte myCompte = dalCompte.modifierCompte(idCompte, utilisateurAmodifier);


                //UtilisateurCompletViewModel utilisateurAmodifier = new UtilisateurCompletViewModel
                //{
                //    Compte = new Compte { Identifiant = identifiant, Password = password },
                //    Profil = new Profil { Email = email, Image = "/img/profil.jpg", Telephone = telephone },
                //    Utilisateur = new Utilisateur { Nom = nom, Prenom = prenom }
                //};

                if (ModelState.IsValid)
                {
                    dal.ModifierUtilisateur(utilisateurAmodifier.Utilisateur);
                    dal.ModifierCompte(utilisateurAmodifier.Compte);
                    dal.ModifierProfil(utilisateurAmodifier.Profil);
                    return RedirectToAction("Index","Home");
                } else
                {
                    return View(utilisateurAmodifier);

                }

            } else
            {
                return Redirect("/Utilisateur/Connexion");
            }
        }

    }
}