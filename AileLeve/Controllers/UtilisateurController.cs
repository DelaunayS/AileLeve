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
        
        [HttpGet()]
        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Inscription(string role,string nom, string prenom, string identifiant, string password,
         string email, string tel, DateTime dateNaissance, int numeroRue, string rue, int codePostal, string ville )
        {

            if (nom != null && prenom != null && identifiant != null && password != null)
            {

                int adresseId=dal.CreerAdresse(numeroRue, rue, codePostal, ville);
                int utilisateurId = dal.CreerUtilisateur(nom, prenom, adresseId);
                dal.CreerEleve(dateNaissance,utilisateurId);
                int profilId = dal.CreerProfil(tel, "/img/profil.jpg", email);                
                int compteId = dal.CreerCompte(identifiant, password, utilisateurId, profilId,role);
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, compteId.ToString()),
                    new Claim(ClaimTypes.Role,role)
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home", new { @id = compteId });
            }
            return Redirect("/Utilisateur/Connexion"); 
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
                if (compte.StatusActif==false){
                    return View();
                }
                if (compte != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, compte.Id.ToString()),
                        new Claim(ClaimTypes.Role, compte.Role)                        
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
                utilisateurCompletViewModel.Adresse = dal.ObtenirToutesLesAdresses().Where(p => p.Id == viewModel.Compte.ProfilId).FirstOrDefault();


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