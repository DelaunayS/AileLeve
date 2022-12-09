using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AileLeve.Controllers
{

    public class UtilisateurController : Controller
    {

        private Dal dal = new Dal();
        [Authorize]
        public IActionResult Liste()
        {
            UtilisateursViewModel uvms = new UtilisateursViewModel
            {
                Utilisateurs = dal.ObtenirTousLesUtilisateurs()
            };
            return View(uvms);
        }

        [HttpGet()]
        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Inscription(string nom, string prenom, string identifiant, string password,
         string email, string tel, DateTime dateNaissance, int numeroRue, string rue, int codePostal, string ville )
        {

            if (ModelState.IsValid)
            {

                int adressesId=dal.CreerAdresse(numeroRue,rue,ville,codePostal);
                int utilisateurId = dal.CreerUtilisateur(nom, prenom,adressesId);
                dal.CreerEleve(dateNaissance,utilisateurId);
                int profilId = dal.CreerProfil(tel, "/img/profil.jpg", email);
                int compteId = dal.CreerCompte(identifiant, password, utilisateurId, profilId);
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, compteId.ToString()),
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home", new { @id = compteId });
            }
            return View();
        }

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("Connexion");
        }

        [HttpGet]
        public IActionResult Connexion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Connexion(string identifiant, string password)
        {
            if (ModelState.IsValid)
            {
                Compte compte = dal.Authentifier(identifiant, password);
                if (compte != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, compte.Id.ToString())
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);
                    return RedirectToAction("Index", "Home", new { @id = compte.Id });
                }
                ModelState.AddModelError("Compte.Identifiant", "Identifiant et/ou mot de passe incorrect(s)");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Supprimer()
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

        [HttpPost]
        public IActionResult Supprimer(UtilisateurCompletViewModel utilisateurASupprimer)
        {
            CompteViewModel viewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };

            if (viewModel.Authentifie)
            {
                viewModel.Compte = dal.ObtenirCompte(HttpContext.User.Identity.Name);
                Compte compte = dal.ObtenirTousLesComptes().Where(p => p.Id == viewModel.Compte.ProfilId).FirstOrDefault();

                if (Dal.EncodeMD5(utilisateurASupprimer.Compte.Password) == compte.Password)

                {
                    dal.SupprimerCompte(compte);

                    return RedirectToAction("Deconnexion");
                }

                return View();
            }
            return Redirect("/Utilisateur/Connexion");
        }
    }
}