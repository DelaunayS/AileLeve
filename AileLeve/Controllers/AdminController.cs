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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private Dal dal = new Dal();
        public IActionResult ListeUtilisateur()
        {
            ComptesViewModel cvm = new ComptesViewModel
            {
                Comptes = dal.ObtenirTousLesComptes()
            };
            return View(cvm);
        }

        public IActionResult Supprimer(int id)
        {
            Compte compteASupprimer = dal.ObtenirCompte(id);
            Utilisateur utilisateur=compteASupprimer.Utilisateur;                 
            
            dal.SupprimerCompte(compteASupprimer);
            dal.SupprimerProfil(compteASupprimer.Profil);
            if (compteASupprimer.Role=="Eleve"){
                int eleveId=dal.ObtenirEleveParUserId(utilisateur.Id);
                dal.SupprimerEtudieParIdEleve(eleveId);
                dal.SupprimerEleveParId(eleveId);
            }
            if (compteASupprimer.Role=="Enseignant"){
                //Pas besoin de supprimer les cours car DeleteOnCascade
                dal.SupprimerEnseignantParId(compteASupprimer.UtilisateurId.Value);
            }
            dal.SupprimerUtilisateur(utilisateur); 
            if (utilisateur.Adresse!=null){
            dal.SupprimerAdresse(compteASupprimer.Utilisateur.Adresse); 
            }   
            return Redirect("/Admin/ListeUtilisateur");
        }


        public IActionResult Suspendre(int id)
        {
            Compte compteASuspendre = dal.ObtenirCompte(id);
            dal.SuspendreCompte(compteASuspendre);

            return Redirect("/Admin/ListeUtilisateur");
        }
    }
}