using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Controllers
{
    [Authorize(Roles = "Admin, Enseignant")]     
    public class EnseignantController : Controller
    {
        private Dal dal = new Dal();

        public IActionResult EmploiDuTempsEnseignant()
        {
            EmploiDuTempsEnseignantViewModel edtevm = new EmploiDuTempsEnseignantViewModel
            {
                ListeEmploiDuTempsEnseignants = dal.ObtenirTousLesEmploisDuTemps()
            };

            return View(edtevm);
        }
    }
}
