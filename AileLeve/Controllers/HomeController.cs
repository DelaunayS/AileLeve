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
                return View(viewModel.Compte);                
            }
            return Redirect("/Utilisateur/Connexion");
        }
    }
}