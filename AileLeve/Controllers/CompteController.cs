using AileLeve.Models;
using AileLeve.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace AileLeve.Controllers
{
    public class CompteController : Controller
    {
        public Dal dal = new Dal();
        [HttpGet]
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
        [HttpPost]
        public IActionResult ModifierPassword(CompteViewModel monCompte, string mdp, string mdpConfirmation)
        {
            CompteViewModel viewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (monCompte == null)
            {
                return View("Error");
            }
            string mdpCrypte = Dal.EncodeMD5(mdp);
            if (viewModel.Authentifie)
            {
                dal.ModifierPassword(monCompte.Compte.Id, mdpCrypte);
                return RedirectToAction("Deconnexion", "Utilisateur");
            }
            else
            {
                return Redirect("/Utilisateur/Connexion");
            }
        }
    }
}