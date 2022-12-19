using AileLeve.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace AileLeve.Models
{
    public class Dal : IDal
    {
        private BddContext _bddContext;
        public Dal()
        {
            _bddContext = new BddContext();
        }
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //!! Les méthodes sont classées par l'ordre alphabétique de leur classe Models

        /*Methodes pour le model Adresse
        */
        public int CreerAdresse(int numeroRue, string rue, int codePostal, string ville)
        {
            Adresse adresse = new Adresse() { NumeroRue = numeroRue, Rue = rue, CodePostal = codePostal, Ville = ville };

            _bddContext.Adresses.Add(adresse);
            _bddContext.SaveChanges();
            return adresse.Id;
        }

        public void AjouterAdresse(int id, Adresse adresse)
        {
            Compte compte = ObtenirCompte(id);
            compte.Utilisateur.AdresseId = CreerAdresse(adresse.NumeroRue, adresse.Rue, adresse.CodePostal, adresse.Ville);
            _bddContext.SaveChanges();
        }
        public List<Adresse> ObtenirToutesLesAdresses()
        {
            return _bddContext.Adresses.ToList();
        }

        public void ModifierAdresse(Adresse adresse)
        {
            _bddContext.Adresses.Update(adresse);
            _bddContext.SaveChanges();
        }
        public void SupprimerAdresse(Adresse adresse)
        {
            _bddContext.Adresses.Remove(adresse);
            _bddContext.SaveChanges();
        }
        public Adresse ObtenirAdresse(int id)
        {
            return this._bddContext.Adresses.Find(id);
        }

        /*Methodes pour le model Compte
        */

      

        public bool RechercherCompte(string identifiant, string password)
        {
            string passwordCrypt = EncodeMD5(password);
            Compte compte = this.ObtenirTousLesComptes().Where(c => c.Identifiant == identifiant && c.Password == passwordCrypt).FirstOrDefault();
            if (compte == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int CreerCompte(string identifiant, string password, int utilisateurId, int profilId, string role)
        {
            string motDePasse = EncodeMD5(password);
            Compte compte = new Compte()
            {
                Identifiant = identifiant,
                Password = motDePasse,
                UtilisateurId = utilisateurId,
                ProfilId = profilId,
                StatusActif = true,
                Role = role
            };
            _bddContext.Comptes.Add(compte);
            _bddContext.SaveChanges();
            return compte.Id;
        }
        public List<Compte> ObtenirTousLesComptes()
        {
            return _bddContext.Comptes.Include(c => c.Profil).Include(c => c.Utilisateur)
                        .ThenInclude(u => u.Adresse).ToList();
        }
        public Compte ObtenirCompte(int id)
        {
            return this._bddContext.Comptes.Include(c => c.Profil).Include(c => c.Utilisateur)
                        .ThenInclude(u => u.Adresse).FirstOrDefault(c => c.Id == id);
        }
        public Compte ObtenirCompte(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirCompte(id);
            }
            return null;
        }
        public void ModifierCompte(Compte compte)
        {
            _bddContext.Comptes.Update(compte);
            _bddContext.SaveChanges();
        }

        public void SupprimerCompte(Compte compte)
        {
            _bddContext.Comptes.Remove(compte);
            _bddContext.SaveChanges();
        }
        public void SuspendreCompte(Compte compte)
        {
            compte.StatusActif = !compte.StatusActif;
            _bddContext.Comptes.Update(compte);
            _bddContext.SaveChanges();
        }
        public List<Compte> ObtenirCompteEleves()
        {
            return this._bddContext.Comptes.Where(c => c.Role.Equals("Eleve")).ToList();
        }
        public Compte Authentifier(string identifiant, string password)
        {
            string motDePasse = EncodeMD5(password);
            Compte compte = this._bddContext.Comptes.FirstOrDefault(
                c => c.Identifiant == identifiant && c.Password == motDePasse);
            return compte;
        }


        /*Methodes pour le model Cours
        */

        public int CreerCoursSimple(TypeCours typeCours, string matiere, string niveau)
        {
            Matiere mat = _bddContext.Matieres.Where(m => m.Nom == matiere).FirstOrDefault();
            Niveau niv = _bddContext.Niveaux.Where(m => m.Nom == niveau).FirstOrDefault();
            Compte admin = ObtenirTousLesComptes().Where(c => c.Role == "Admin").FirstOrDefault();
            Enseignant enseignantAdmin = this.ObtenirTousLesEnseignants().Where(i => i.UtilisateurId == admin.Utilisateur.Id).FirstOrDefault();

            Cours cours = new Cours
            {
                TypeCours = typeCours,
                Matiere = mat,
                Niveau = niv
            };

            if (enseignantAdmin == null)
            {
                int ensId = this.CreerEnseignant(admin.Utilisateur.Id);
                Enseignant ens = this.ObtenirTousLesEnseignants().Where(i => i.Id == ensId).FirstOrDefault();
                cours.Enseignant = ens;
            }
            else
            {
                cours.Enseignant = enseignantAdmin;
            };

            _bddContext.Cours.Add(cours);
            _bddContext.SaveChanges();
            return cours.Id;
        }


        public int CreerCours(TypeCours typeCours, string matiere, string niveau, int id)
        {
            Matiere mat = _bddContext.Matieres.Where(m => m.Nom == matiere).FirstOrDefault();
            Niveau niv = _bddContext.Niveaux.Where(m => m.Nom == niveau).FirstOrDefault();
            Enseignant ens = this.ObtenirTousLesEnseignants().Where(i => i.Id == id).FirstOrDefault();


            Cours cours = new Cours
            {
                TypeCours = typeCours,
                Matiere = mat,
                Niveau = niv,
                Enseignant = ens,

            };
            _bddContext.Cours.Add(cours);
            _bddContext.SaveChanges();
            return cours.Id;
        }
        public Cours ObtenirCours(int id)
        {
            return this._bddContext.Cours.Include(c => c.Matiere).Include(c => c.Niveau)
                       .Include(u => u.Enseignant).ThenInclude(c => c.Utilisateur).FirstOrDefault(c => c.Id == id);
        }
        public List<Cours> ObtenirCoursParEnseignant(int id)
        {
            return this._bddContext.Cours.Where(u => u.EnseignantId == id).Include(c => c.Matiere).Include(c => c.Niveau)
                       .Include(u => u.Enseignant).ToList();
        }
        public void ModifierCours(Cours cours)
        {

            _bddContext.Cours.Update(cours);
            _bddContext.SaveChanges();
        }


        public void ModifierEnseignantDuCours(int idCours, int idEnseignant)
        {
            Cours cours = this._bddContext.Cours.Find(idCours);
            cours.EnseignantId = idEnseignant;
            _bddContext.SaveChanges();

        }


        public List<Cours> ObtenirTousLesCours()
        {
            return this._bddContext.Cours
                .Include(c => c.Matiere)
                .Include(c => c.Niveau)
                .Include(u => u.Enseignant).ToList();
        }


        public void SupprimerCours(Cours cours)
        {
            _bddContext.Cours.Remove(cours);
            _bddContext.SaveChanges();
        }

        public (List<EstDisponible>, List<Etudie>) AdminObtenirCoursProposesAvecDateEtEleve()
        {
            var coursDeLEnseignant = this._bddContext.EstDisponible
                .Include(c => c.Cours)
                .Include(c => c.Cours.Matiere)
                .Include(c => c.Cours.Niveau)
                .Include(c => c.EmploiDuTempsEnseignant)
                .Include(c => c.Enseignant)
                .Include(c => c.Enseignant.Utilisateur).ToList();

            List<Etudie> etudieList = new List<Etudie>();

            foreach (var item in coursDeLEnseignant)
            {
                var query2 = this._bddContext.Etudie.Where(c => c.CoursId == item.CoursId)
                .Include(c => c.Eleve)
                .ThenInclude(c => c.Utilisateur)
                .ThenInclude(c => c.Adresse).ToList();
                etudieList = etudieList.Concat(query2).ToList();
            }

            return (coursDeLEnseignant, etudieList);
        }

        public (List<EstDisponible>, List<Etudie>) ObtenirCoursProposesAvecDateEtEleve(int id)
        {
            var coursDeLEnseignant = this._bddContext.EstDisponible.Where(c => c.EnseignantId == id)
                .Include(c => c.Cours)
                .Include(c => c.Cours.Matiere)
                .Include(c => c.Cours.Niveau)
                .Include(c => c.EmploiDuTempsEnseignant)
                .Include(c => c.Enseignant).ToList();

            List<Etudie> etudieList = new List<Etudie>();

            foreach (var item in coursDeLEnseignant)
            {
                var query2 = this._bddContext.Etudie.Where(c => c.CoursId == item.CoursId)
                .Include(c => c.Eleve)
                .ThenInclude(c => c.Utilisateur)
                .ThenInclude(c => c.Adresse).ToList();
                etudieList = etudieList.Concat(query2).ToList();
            }

            return (coursDeLEnseignant, etudieList);
        }

        public List<Etudie> ObtenirTousLesEtudie()
        {
            return this._bddContext.Etudie
                .Include(c => c.Cours)
                .Include(c => c.Cours.Matiere)
                .Include(c => c.Cours.Niveau)
                .Include(c => c.Cours.Enseignant)
                .Include(c => c.Cours.Enseignant.Utilisateur)
                .Include(c => c.Cours.Enseignant.Utilisateur.Adresse).ToList();
        }

        public (List<Etudie>, List<EstDisponible>) ObtenirCoursReservesAvecDateEtProf(int id)
        {
            var coursDeLEleve = this._bddContext.Etudie.Where(c => c.EleveId == id)
                .Include(c => c.Cours)
                .Include(c => c.Cours.Enseignant)
                .Include(c => c.Cours.Enseignant.Utilisateur)
                .Include(c => c.Cours.Matiere)
                .Include(c => c.Cours.Niveau)
                .Include(c => c.Eleve)
                .Include(c => c.Eleve.Utilisateur)
                .Include(c => c.Eleve.Utilisateur.Adresse).ToList();

            List<EstDisponible> EstDisponibleList = new List<EstDisponible>();

            foreach (var item in coursDeLEleve)
            {
                var query2 = this._bddContext.EstDisponible.Where(c => c.CoursId == item.CoursId)
                .Include(c => c.EmploiDuTempsEnseignant).ToList();
                EstDisponibleList = EstDisponibleList.Concat(query2).ToList();
            }

            return (coursDeLEleve, EstDisponibleList);

        }

        public List<Cours> ObtenirCoursParEnseignantPourAdmin()
        {
            return this._bddContext.Cours.Include(c => c.Matiere).Include(c => c.Niveau)
                       .Include(u => u.Enseignant).ToList();

        }

        public Cours ObtenirCours(string idStr)
        {
            int id;
            {
                if (int.TryParse(idStr, out id))
                    return this.ObtenirCours(id);
            }
            return null;
        }
        public void SupprimerCoursParIdEnseignant(int enseignantId)
        {
            List<Cours> cours = _bddContext.Cours.Where(c => c.EnseignantId == enseignantId).ToList();
            _bddContext.Cours.RemoveRange(cours);
            _bddContext.SaveChanges();
        }

        //Methodes pour le model Eleve
        public int CreerEleve(DateTime date, int utilisateurId)
        {
            Eleve eleve = new Eleve() { DateDeNaissance = date, UtilisateurId = utilisateurId };
            _bddContext.Eleves.Add(eleve);
            _bddContext.SaveChanges();
            return eleve.Id;
        }

        public void SupprimerEleve(Eleve eleve)
        {
            _bddContext.Eleves.Remove(eleve);
            _bddContext.SaveChanges();
        }
        public void SupprimerEleveParId(int eleveId)
        {
            Eleve eleve = _bddContext.Eleves.Find(eleveId);
            _bddContext.Eleves.Remove(eleve);
            _bddContext.SaveChanges();
        }
        public List<Eleve> ObtenirTousLesEleves()
        {
            return this._bddContext.Eleves.ToList();
        }

        public Eleve ObtenirEleve(int id)
        {
            return this._bddContext.Eleves.FirstOrDefault(c => c.Id == id);
        }
         public int ObtenirEleveParUserId(int userId)
        {
            Eleve eleve = _bddContext.Eleves.Where(e => e.UtilisateurId == userId).FirstOrDefault();
            return eleve.Id;
        }

        /*Methodes pour le model EmploiDuTempsEnseignant
        */
        public int CreerEmploiDuTemps(DateTime date)
        {
            EmploiDuTempsEnseignant emploiDuTemps = new EmploiDuTempsEnseignant() { DateTime = date, Disponible = true };
            _bddContext.EmploiDuTempsEnseignants.Add(emploiDuTemps);
            _bddContext.SaveChanges();
            return emploiDuTemps.Id;
        }
        public List<EmploiDuTempsEnseignant> ObtenirTousLesEmploisDuTemps()
        {
            return _bddContext.EmploiDuTempsEnseignants.ToList();
        }
        public void ModifierEmploiDuTemps(EmploiDuTempsEnseignant edt)
        {
            _bddContext.EmploiDuTempsEnseignants.Update(edt);
            _bddContext.SaveChanges();
        }


        //Methodes pour le model Enseignant
        public int CreerEnseignant(int utilisateurId)
        {
            Enseignant enseignant = new Enseignant() { UtilisateurId = utilisateurId };
            _bddContext.Enseignants.Add(enseignant);
            _bddContext.SaveChanges();
            return enseignant.Id;
        }
        public List<Enseignant> ObtenirTousLesEnseignants()
        {
            return _bddContext.Enseignants.ToList();
        }
        public void SupprimerEnseignant(Enseignant enseignant)
        {
            _bddContext.Enseignants.Remove(enseignant);
            _bddContext.SaveChanges();
        }
        public void SupprimerEnseignantParId(int userId)
        {
            Enseignant enseignant = _bddContext.Enseignants.Where(e => e.UtilisateurId == userId).FirstOrDefault();
            _bddContext.Enseignants.Remove(enseignant);
            _bddContext.SaveChanges();
        }


        //Methodes pour le model EstDisponible
        public void CreerEstDisponible(int enseignantId, int emploiDuTempsId, int coursId)
        {
            EstDisponible dispo = new EstDisponible() { EnseignantId = enseignantId, EmploiDuTempsEnseignantId = emploiDuTempsId, CoursId = coursId };
            _bddContext.EstDisponible.Add(dispo);
            _bddContext.SaveChanges();
        }

        public void EstDisponible(int id)
        {
            EmploiDuTempsEnseignant edt = this.ObtenirTousLesEmploisDuTemps().Where(c => c.Id == id).FirstOrDefault();
            edt.Disponible = true;
            _bddContext.SaveChanges();
        }
        public void NestPlusDisponible(int id)
        {
            EmploiDuTempsEnseignant edt = this.ObtenirTousLesEmploisDuTemps().Where(c => c.Id == id).FirstOrDefault();
            edt.Disponible = false;
            _bddContext.SaveChanges();
        }
        public List<EstDisponible> ObtenirToutesLesDispos()
        {
            return _bddContext.EstDisponible.ToList();
        }


        public void SupprimerEstDisponible(EstDisponible estDisponible)
        {
            _bddContext.EstDisponible.Remove(estDisponible);
            _bddContext.SaveChanges();
        }

        //Methodes pour le model Etudie


        public void CreerEtudie(int eleveId, int coursId)
        {
            Etudie etudie = new Etudie() { EleveId = eleveId, CoursId = coursId };
            _bddContext.Etudie.Add(etudie);
            _bddContext.SaveChanges();
        }

        public List<Etudie> ObtenirToutesLesEtudie()
        {
            return this._bddContext.Etudie
                .Include(c => c.Cours)
                .Include(c => c.Cours.Matiere)
                .Include(c => c.Cours.Niveau)
                .Include(c => c.Cours.Enseignant)
                .ToList();
        }
        public void SupprimerEtudie(Etudie etudie)
        {
            _bddContext.Etudie.Remove(etudie);
            _bddContext.SaveChanges();
        }
        public void SupprimerEtudieParIdEleve(int eleveId)
        {
            List<Etudie> etudies = _bddContext.Etudie.Where(e => e.EleveId == eleveId).ToList();
            _bddContext.Etudie.RemoveRange(etudies);
            _bddContext.SaveChanges();
        }

        //Methodes pour le model Matiere
        public int creerMatiere(string nom){
             Matiere matiere = new Matiere { Nom = nom};       
            _bddContext.Matieres.Add(matiere);
            _bddContext.SaveChanges();
            return matiere.Id;
        }
        public Matiere ObtenirMatiere(int id)
        {
            return this._bddContext.Matieres.FirstOrDefault(c => c.Id == id);
        }


        //Methodes pour le model Niveau
        public int creerNiveau(string nom){
             Niveau niveau = new Niveau { Nom = nom};       
            _bddContext.Niveaux.Add(niveau);
            _bddContext.SaveChanges();
            return niveau.Id;
        }
        public Niveau ObtenirNiveau(int id)
        {
            return this._bddContext.Niveaux.FirstOrDefault(c => c.Id == id);
        }

        //Methodes pour le model Notification
        public int CreerNotification(string nom)
        {
            Notification notif = new Notification
            {
                Lu = false,
                TypeNotification = nom
            };
            _bddContext.Notifications.Add(notif);
            _bddContext.SaveChanges();
            return notif.Id;
        }
        public List<Notification> ObtenirNotifications()
        {
            return this._bddContext.Notifications.ToList();
        }

        public void SupprimerNotification(Notification notification)
        {
            this._bddContext.Notifications.Remove(notification);
            this._bddContext.SaveChanges();
        }


        /*Methodes pour le model Profil
        */

        public int CreerProfil(string telephone, string image, string email)
        {
            Profil profil = new Profil() { Telephone = telephone, Image = image, Email = email };
            _bddContext.Profils.Add(profil);
            _bddContext.SaveChanges();
            return profil.Id;
        }
        public List<Profil> ObtenirTousLesProfils()
        {
            return _bddContext.Profils.ToList();
        }
        public Profil ObtenirProfil(int id)
        {
            return this._bddContext.Profils.Find(id);
        }
        public void ModifierProfil(Profil profil)
        {
            _bddContext.Profils.Update(profil);
            _bddContext.SaveChanges();
        }
        public void SupprimerProfil(Profil profil)
        {
            _bddContext.Profils.Remove(profil);
            _bddContext.SaveChanges();
        }

        //Methodes pour le model RepresentantLegal

        public int CreerRepresentantLegal(string nomRL, string prenomRL, int eleveId)
        {
            RepresentantLegal representantLegal = new RepresentantLegal() { NomRL = nomRL, PrenomRL = prenomRL, EleveId = eleveId };
            _bddContext.RepresentantLegaux.Add(representantLegal);
            _bddContext.SaveChanges();
            return representantLegal.Id;
        }
        public List<RepresentantLegal> ObtenirTousLesRepresentantsLegaux()
        {
            return _bddContext.RepresentantLegaux.ToList();
        }

        public void AjouterRL(int id, RepresentantLegal representantLegal)
        {
            Eleve eleve = ObtenirEleve(id);
            representantLegal.Eleve = eleve;
            _bddContext.SaveChanges();

        }

        public void ModifierRL(RepresentantLegal representantLegal)
        {
            _bddContext.RepresentantLegaux.Update(representantLegal);
            _bddContext.SaveChanges();
        }

        /*Methodes pour le model Utilisateur
        */

        public int CreerUtilisateur(string nom, string prenom, int adresseId)
        {
            Utilisateur utilisateur = new Utilisateur() { Nom = nom, Prenom = prenom, AdresseId = adresseId };
            _bddContext.Utilisateurs.Add(utilisateur);
            _bddContext.SaveChanges();
            return utilisateur.Id;
        }
        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            return _bddContext.Utilisateurs.ToList();
        }
        public void SupprimerUtilisateur(Utilisateur utilisateur)
        {
            _bddContext.Utilisateurs.Remove(utilisateur);
            _bddContext.SaveChanges();
        }
        public Utilisateur ObtenirUtilisateur(int id)
        {
            return this._bddContext.Utilisateurs.Find(id);
        }
        
        public void ModifierUtilisateur(Utilisateur utilisateur)
        {
            _bddContext.Utilisateurs.Update(utilisateur);
            _bddContext.SaveChanges();
        }

        /*Pour modifier la date de naissance de l'eleve
        */
        public void ModifierDateNaissance(int id, string dateDeNaissance)
        {
            Eleve elv = _bddContext.Eleves.Where(c => c.Id == id).FirstOrDefault();
            CultureInfo culture = new CultureInfo("fr-FR");
            elv.DateDeNaissance = DateTime.Parse(dateDeNaissance, provider: culture);
            _bddContext.SaveChanges();
        }

        /*Envoi de mail
        */
        public void EnvoyerMail(string mailUtilisateur, MailMessage mail)
        {
            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("aileleve.soutienscolaire@gmail.com", "sfxftocrnfsjaigq")
            };
            email.Send(mail);
        }

       
        /* Methode pour obtenir tous les plannings
        */
        public List<EstDisponible> ObtenirTousLesPlannings()
        {
            return this._bddContext.EstDisponible
                .Include(c => c.Cours)
                .Include(c => c.Cours.Matiere)
                .Include(c => c.Cours.Niveau)
                .Include(c => c.Cours.Enseignant)
                .Include(c => c.Cours.Enseignant.Utilisateur)
                .Include(c => c.Enseignant)
                .Include(c => c.Enseignant.Utilisateur)
                .Include(c => c.EmploiDuTempsEnseignant)
                .ToList();
        }

        /* Methode pour obtenir tous les cours réservés
        */
        public List<Etudie> ObtenirToutesLesReservationsCours()
        {
            return _bddContext.Etudie
                .Include(c => c.Eleve)
                .Include(c => c.Eleve.Utilisateur)
                .Include(c => c.Eleve.Utilisateur.Adresse)
                .Include(c => c.Cours)
                .Include(c => c.Cours.Matiere)
                .Include(c => c.Cours.Niveau)
                .Include(c => c.Cours.Enseignant)
                .Include(c => c.Cours.Enseignant.Utilisateur)
                .ToList();
        }

        /* Methode pour encoder le mot de passe
        */
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        /* Methode pour modifier le mot de passe
                */
        public void ModifierPassword(int id, string nouveauMDP)
        {
            string mdp = EncodeMD5(nouveauMDP);
            Compte compte = this.ObtenirCompte(id);
            compte.Password = nouveauMDP;
            _bddContext.SaveChanges();
        }

    }
}

