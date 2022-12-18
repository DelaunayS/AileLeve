using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
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

        public IActionResult InfosContact(int id)
        {

            CompteViewModel viewModel = new CompteViewModel();
            viewModel.Utilisateur = dal.ObtenirUtilisateur(id);
            viewModel.Compte = dal.ObtenirTousLesComptes().Where(e => e.Id == viewModel.Utilisateur.Id).FirstOrDefault();
            viewModel.Profil = dal.ObtenirTousLesProfils().Where(e => e.Id == viewModel.Compte.ProfilId).FirstOrDefault();

            if (viewModel.Compte.Role=="Enseignant")
                {
                    viewModel.Enseignant = dal.ObtenirTousLesEnseignants().Where(e => e.UtilisateurId == viewModel.Compte.UtilisateurId).FirstOrDefault();
                    viewModel.EstDisponible = dal.ObtenirToutesLesDispos().Where(e => e.EnseignantId == viewModel.Enseignant.Id).FirstOrDefault();
                    viewModel.EmpEns = dal.ObtenirTousLesEmploisDuTemps().Where(e => e.Id == viewModel.EstDisponible.EmploiDuTempsEnseignantId).FirstOrDefault();
                    viewModel.CoursListe = dal.ObtenirCoursProposesAvecDateEtEleve(viewModel.Enseignant.Id);
                };

                if (viewModel.Compte.Role == "Eleve")
                {
                    viewModel.Eleve = dal.ObtenirTousLesEleves().Where(e => e.UtilisateurId == viewModel.Compte.UtilisateurId).FirstOrDefault();
                    viewModel.CoursListeEleve = dal.ObtenirCoursReservesAvecDateEtProf(viewModel.Eleve.Id);
                };

                return View(viewModel);
            
        }

        public IActionResult SupprimerCoursAdmin(int id)
        {
            Cours cours = dal.ObtenirTousLesCours().Where(c => c.Id == id).FirstOrDefault();
            Etudie reservation = dal.ObtenirTousLesEtudie().Where(c => c.CoursId == cours.Id).FirstOrDefault();
            EstDisponible proposition = dal.ObtenirTousLesPlannings().Where(c => c.CoursId == cours.Id).FirstOrDefault();

            dal.SupprimerEtudie(reservation);
            dal.SupprimerEstDisponible(proposition);
            dal.SupprimerCours(cours);

            return Redirect("Enseignant/EmploiDuTempsEnseignant");
        }


        public IActionResult Supprimer(int id)
        {
            Compte compteASupprimer = dal.ObtenirCompte(id);
            Utilisateur utilisateur = compteASupprimer.Utilisateur;

            dal.SupprimerCompte(compteASupprimer);
            dal.SupprimerProfil(compteASupprimer.Profil);
            if (compteASupprimer.Role == "Eleve")
            {
                int eleveId = dal.ObtenirEleveParUserId(utilisateur.Id);
                dal.SupprimerEtudieParIdEleve(eleveId);
                dal.SupprimerEleveParId(eleveId);
            }
            if (compteASupprimer.Role == "Enseignant")
            {
                //Pas besoin de supprimer les cours car DeleteOnCascade
                dal.SupprimerEnseignantParId(compteASupprimer.UtilisateurId.Value);
            }

            dal.SupprimerUtilisateur(utilisateur); 
            if (utilisateur.Adresse!=null){
            dal.SupprimerAdresse(compteASupprimer.Utilisateur.Adresse); 
            }

            MailMessage message = new MailMessage();
            message.From = new MailAddress("aileleve.soutienscolaire@gmail.com");
            message.Subject = "Votre compte a définitivement supprimé";
            message.Body = "Bonjour " + compteASupprimer.Utilisateur.Prenom + "," + "\n"
                + "Conformément à votre demande, votre compte a été définitivement supprimé. " + "\n" +
                " Nous regrettons votre départ et sommes très attentifs aux raisons qui ont motivé ce choix." + "\n" +
                " Pour cette raison, et afin d'améliorer la qualité de nos services, nous vous prions de prendre " + "\n" +
                " quelques minutes pour répondre au questionnaire que vous recevrez après ce mail. " + "\n" +
                " Equipe d'Aile'Lève";


            message.To.Add(compteASupprimer.Profil.Email);

            dal.EnvoyerMail(compteASupprimer.Profil.Email, message);


            return Redirect("/Admin/ListeUtilisateur");
        }

        public IActionResult Suspendre(int id)
        {
            Compte compteASuspendre = dal.ObtenirCompte(id);
            dal.SuspendreCompte(compteASuspendre);

            return Redirect("/Admin/ListeUtilisateur");
        }
        public IActionResult Valider(int id)
        {
            Compte compteAValider = dal.ObtenirCompte(id);
            return View(compteAValider);
        }
        public IActionResult ValiderEnseignant(int id)
        {


            Compte compteAValider=dal.ObtenirCompte(id);           
            compteAValider.Role="Enseignant";

            dal.ModifierCompte(compteAValider);

            MailMessage message = new MailMessage();
            message.From = new MailAddress("aileleve.soutienscolaire@gmail.com");
            message.Subject = "Votre compte a été validé";
            message.Body = "Bonjour " + compteAValider.Utilisateur.Prenom + "," + "\n" + " Votre compte utilisateur a été validé. " + "\n"
                + " Vous pouvez proposer des cours et mettre à jour votre planning." + "\n" +
                " Notre équipe se tient à votre disposition en cas de besoin. " + "\n" +
                " Equipe d'Aile'Lève";

            message.To.Add(compteAValider.Profil.Email);

            dal.EnvoyerMail(compteAValider.Profil.Email, message);

            return Redirect("/Admin/ListeUtilisateur");

        }
    }
}