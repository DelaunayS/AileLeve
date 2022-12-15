using AileLeve.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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


        public int CreerUtilisateur(string nom, string prenom, int adresseId)
        {
            Utilisateur utilisateur = new Utilisateur() { Nom = nom, Prenom = prenom, AdresseId = adresseId };
            _bddContext.Utilisateurs.Add(utilisateur);
            _bddContext.SaveChanges();
            return utilisateur.Id;
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
                Role=role
            };
            _bddContext.Comptes.Add(compte);
            _bddContext.SaveChanges();
            return compte.Id;
        }


        public int CreerProfil(string telephone, string image, string email)
        {
            Profil profil = new Profil() { Telephone = telephone, Image = image, Email = email };
            _bddContext.Profils.Add(profil);
            _bddContext.SaveChanges();
            return profil.Id;
        }

        public int CreerAdresse(int numeroRue, string rue, int codePostal, string ville)
        {
            Adresse adresse = new Adresse() { NumeroRue = numeroRue, Rue = rue, CodePostal = codePostal, Ville = ville };

            _bddContext.Adresses.Add(adresse);
            _bddContext.SaveChanges();
            return adresse.Id;
        }
        public int CreerEleve(DateTime date, int utilisateurId)
        {
            Eleve eleve = new Eleve() { DateDeNaissance = date, UtilisateurId = utilisateurId };
            _bddContext.Eleves.Add(eleve);
            _bddContext.SaveChanges();
            return eleve.Id;

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
                Enseignant = ens
            };
            _bddContext.Cours.Add(cours);
            _bddContext.SaveChanges();
            return cours.Id;
        }

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

        public void SupprimerNotification(Notification notification)
        {
            this._bddContext.Notifications.Remove(notification);
            this._bddContext.SaveChanges();
        }

        public int CreerEnseignant(int utilisateurId)
        {
            Enseignant enseignant = new Enseignant() { UtilisateurId = utilisateurId };
            _bddContext.Enseignants.Add(enseignant);
            _bddContext.SaveChanges();
            return enseignant.Id;
        }


        public void AjouterAdresse(int id, Adresse adresse)
        {
            Compte compte = ObtenirCompte(id);
            compte.Utilisateur.AdresseId = CreerAdresse(adresse.NumeroRue, adresse.Rue, adresse.CodePostal, adresse.Ville);
            _bddContext.SaveChanges();
        }

        

        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            return _bddContext.Utilisateurs.ToList();
        }
        public List<Compte> ObtenirTousLesComptes()
        {
            return _bddContext.Comptes.Include(c => c.Profil).Include(c => c.Utilisateur)
                        .ThenInclude(u => u.Adresse).ToList();
        }
        public List<Profil> ObtenirTousLesProfils()
        {
            return _bddContext.Profils.ToList();
        }

        public List<Adresse> ObtenirToutesLesAdresses()
        {
            return _bddContext.Adresses.ToList();
        }

        public void ModifierDateNaissance(int id, string dateDeNaissance)
        {
            Eleve elv = _bddContext.Eleves.Where(c => c.Id == id).FirstOrDefault();
            CultureInfo culture = new CultureInfo("fr-FR");
            elv.DateDeNaissance = DateTime.Parse(dateDeNaissance, provider: culture);
            _bddContext.SaveChanges();
        }

        public void EnvoyerMailInscription(string mailUtilisateur, MailMessage mail)
        {
            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("aileleve.soutienscolaire@gmail.com", "ahjwdxnfsajcgyws")
            };

            email.Send(mail);
        
        }

        public void ModifierAdresse(Adresse adresse)
        {
            _bddContext.Adresses.Update(adresse);
            _bddContext.SaveChanges();
        }
        

        public void SupprimerCompte(Compte compte)
        {
            _bddContext.Comptes.Remove(compte);
            _bddContext.SaveChanges();
        }
        public void SupprimerProfil(Profil profil)
        {
            _bddContext.Profils.Remove(profil);
            _bddContext.SaveChanges();
        }
        public void SupprimerUtilisateur(Utilisateur utilisateur)
        {
            _bddContext.Utilisateurs.Remove(utilisateur);
            _bddContext.SaveChanges();
        }
        public void SupprimerAdresse(Adresse adresse)
        {
            _bddContext.Adresses.Remove(adresse);
            _bddContext.SaveChanges();
        }
        public void SupprimerEleve(Eleve eleve)
        {
            _bddContext.Eleves.Remove(eleve);
            _bddContext.SaveChanges();
        }
        public void SupprimerEleveParId(int eleveId)
        {
            Eleve eleve=_bddContext.Eleves.Find(eleveId);
            _bddContext.Eleves.Remove(eleve);
            _bddContext.SaveChanges();
        }
        public void SupprimerEnseignant(Enseignant enseignant)
        {
            _bddContext.Enseignants.Remove(enseignant);
            _bddContext.SaveChanges();
        }
        public void SupprimerEnseignantParId(int userId)
        {
            Enseignant enseignant=_bddContext.Enseignants.Where(e=>e.UtilisateurId == userId).FirstOrDefault();
            _bddContext.Enseignants.Remove(enseignant);
            _bddContext.SaveChanges();
        }
        public void SupprimerCoursParIdEnseignant(int enseignantId){
            List<Cours> cours=_bddContext.Cours.Where(c => c.EnseignantId==enseignantId).ToList();
            _bddContext.Cours.RemoveRange(cours);
            _bddContext.SaveChanges();
        }
        public void SupprimerEtudieParIdEleve(int eleveId){
            List<Etudie> etudies=_bddContext.Etudie.Where(e => e.EleveId==eleveId).ToList();
            _bddContext.Etudie.RemoveRange(etudies);
            _bddContext.SaveChanges();
        }
        public int ObtenirEleveParUserId(int userId){
            Eleve eleve=_bddContext.Eleves.Where(e=>e.UtilisateurId == userId).FirstOrDefault();
            return eleve.Id;
        }
       
        public List<Enseignant> ObtenirTousLesEnseignants()
        {
            return _bddContext.Enseignants.ToList();
        }

        


       public List<Notification> ObtenirNotifications()
        {
            return this._bddContext.Notifications.ToList();
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return this._bddContext.Utilisateurs.Find(id);
        }
        public void EffacerUtilisateur(int id, string role)
        {
            this._bddContext.Utilisateurs.Find(id);  
        }
        
        public Profil ObtenirProfil(int id)
        {
            return this._bddContext.Profils.Find(id);
        }
        public Adresse ObtenirAdresse(int id)
        {
            return this._bddContext.Adresses.Find(id);
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

        public Cours ObtenirCours(int id)
        {
            return this._bddContext.Cours.Include(c => c.Matiere).Include(c => c.Niveau)
                       .Include(u => u.Enseignant).FirstOrDefault(c => c.Id == id);
        }
        public List<Cours> ObtenirCoursParEnseignant(int id)
        {
            return this._bddContext.Cours.Where(u => u.EnseignantId == id).Include(c => c.Matiere).Include(c => c.Niveau)
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

        public List<EmploiDuTempsEnseignant> ObtenirTousLesEmploisDuTemps()
        {
            return _bddContext.EmploiDuTempsEnseignants.ToList();
        }



        public Matiere ObtenirMatiere(int id)
        {
            return this._bddContext.Matieres.FirstOrDefault(c => c.Id == id);
        }

        public Niveau ObtenirNiveau(int id)
        {
            return this._bddContext.Niveaux.FirstOrDefault(c => c.Id == id);
        }
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }


        public List<Eleve> ObtenirTousLesEleves()
        {
            return this._bddContext.Eleves.ToList();
        }

        public Eleve ObtenirEleve(int id)
        {
            return this._bddContext.Eleves.FirstOrDefault(c => c.Id == id);
        }
        public List<Compte> ObtenirCompteEleves()
        {
            return this._bddContext.Comptes.Where(c => c.Role.Equals("Eleve")).ToList();
        }


        public void ModifierProfil(Profil profil)
        {
            _bddContext.Profils.Update(profil);
            _bddContext.SaveChanges();
        }
        public void ModifierCompte(Compte compte)
        {
            _bddContext.Comptes.Update(compte);
            _bddContext.SaveChanges();
        }
        public void ModifierUtilisateur(Utilisateur utilisateur)
        {
            _bddContext.Utilisateurs.Update(utilisateur);
            _bddContext.SaveChanges();
        }

       

        public void ModifierPassword(int id, string nouveauMDP)
        {
            string mdp = EncodeMD5(nouveauMDP);
            Compte compte = this.ObtenirCompte(id);
            compte.Password = nouveauMDP;
            _bddContext.SaveChanges();
        }


        public void ModifierCours(Cours cours)
        {
           
            _bddContext.Cours.Update(cours);
            _bddContext.SaveChanges();
        }

        public List<Cours> ObtenirTousLesCours()
        {
            return this._bddContext.Cours.Include(c => c.Matiere).Include(c => c.Niveau)
                       .Include(u => u.Enseignant).ToList();
        }
       

        public void SupprimerCours(Cours cours)
        {
            _bddContext.Cours.Remove(cours);
            _bddContext.SaveChanges();
        }
        public void SuspendreCompte(Compte compte)
        {
            compte.StatusActif = !compte.StatusActif;
            _bddContext.Comptes.Update(compte);
            _bddContext.SaveChanges();
        }

       

        public Compte Authentifier(string identifiant, string password)
        {
            string motDePasse = EncodeMD5(password);
            Compte compte = this._bddContext.Comptes.FirstOrDefault(
                c => c.Identifiant == identifiant && c.Password == motDePasse);
            return compte;
        }
        
    }
}
