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
        public IActionResult Liste()       {
            

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
        public IActionResult Inscription(string nom, string prenom, string identifiant, string password, string email)
        {
           
            if (ModelState.IsValid)
            {
                int utilisateurId = dal.CreerUtilisateur(nom, prenom);
                int profilId = dal.CreerProfil("", "", email);
                int compteId = dal.CreerCompte(identifiant, password, utilisateurId, profilId);
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, compteId.ToString()),
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Liste");
            }
            return View();
        }

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("Connexion");
        }

        [HttpGet()]
        public IActionResult Connexion()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Connexion(string identifiant, string password)
        {
            if (ModelState.IsValid)
            {
                Compte compte = dal.Authentifier(identifiant, password);
                if (compte!= null)
                {var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, compte.Id.ToString())
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);
                    return Redirect("Liste");
                }
            ModelState.AddModelError("Compte.Identifiant", "Identifiant et/ou mot de passe incorrect(s)");
            }
            return View();
        }
       




    }
}