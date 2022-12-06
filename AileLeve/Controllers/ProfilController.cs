using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AileLeve.Controllers
{

    public class ProfilController : Controller
    {
        public IActionResult ModifierProfil(int id)
        {
            
            if (id != 0)
            {
                using (Dal dal = new Dal())
                {
                    Profil profil = dal.ObtenirTousLesProfils().Where(r => r.Id == id).FirstOrDefault();
                    if (profil == null)
                    {
                        return View("Error");
                    }
                    return View(profil);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierProfil(Profil profil)
        {
            if (!ModelState.IsValid)
                 return View(profil);
            if (profil.Id != 0)
            {
                using (Dal dal = new Dal())
                {
                    dal.ModifierProfil(profil);
                    return RedirectToAction("ModifierProfil", new { @id = profil.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }

    }
}