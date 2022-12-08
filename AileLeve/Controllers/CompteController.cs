using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Controllers
{
    public class CompteController : Controller
    {
        public Dal dal = new Dal();

        public IActionResult ModifierPassword(int id)
        {
            if (id != 0)
            {
                CompteViewModel viewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
                if (viewModel.Authentifie)
                {
                    viewModel.Compte = dal.ObtenirCompte(id);
             
                    return View(viewModel);
                }
                return Redirect("/Utilisateur/Connexion");
            }
            return View("Error");
        }

       



    }
}
