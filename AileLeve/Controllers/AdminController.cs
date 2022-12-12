using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AileLeve.Controllers
{
    
    public class AdminController : Controller
    {
        private Dal dal = new Dal();
        [Authorize]
        public IActionResult ListeUtilisateur()
        {
            ComptesViewModel cvm = new ComptesViewModel
            {
                Comptes = dal.ObtenirTousLesComptes()
            };
                                                   
            return View(cvm);
        }
        [Authorize]

        public IActionResult Supprimer(int id)
        {
            Compte compteASupprimer = dal.ObtenirCompte(id);
            dal.SupprimerCompte(compteASupprimer);
                                                   
            return Redirect("/Admin/ListeUtilisateur");
        }


    }
}