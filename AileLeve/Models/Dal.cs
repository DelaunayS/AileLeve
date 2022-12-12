using AileLeve.ViewModels;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int CreerCompte(string identifiant, string password, int utilisateurId, int profilId)
        {
            string motDePasse = EncodeMD5(password);
            Compte compte = new Compte()
            {
                Identifiant = identifiant,
                Password = motDePasse,
                UtilisateurId = utilisateurId,
                ProfilId = profilId,
                statusActif = true
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

        public int CreerAdresse (int numeroRue, string rue, int codePostal, string ville)
        {
            Adresse adresse = new Adresse() { NumeroRue = numeroRue, Rue=rue, CodePostal = codePostal, Ville =ville};

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

        public void ModifierAdresse(Adresse adresse)
        {
            _bddContext.Adresses.Update(adresse);
            _bddContext.SaveChanges();
        }
        public void AjouterAdresse(int id, Adresse adresse)
        {
            Compte compte = ObtenirCompte(id);
            compte.Utilisateur.AdresseId = CreerAdresse(adresse.NumeroRue, adresse.Rue, adresse.CodePostal, adresse.Ville);
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

        public void SuspendreCompte(Compte compte)
        {
            compte.statusActif = !compte.statusActif;
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
        public Utilisateur ObtenirUtilisateur(int id)
        {
            return this._bddContext.Utilisateurs.Find(id);
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
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
        public void ModifierPassword(int id, string nouveauMDP)
        {
            string mdp = EncodeMD5(nouveauMDP);
            Compte compte = this.ObtenirCompte(id);
            compte.Password = nouveauMDP;
            _bddContext.SaveChanges();
        }

        public int CreerCours(TypeCours typeCours, string matiere, string niveau, string enseignant)
        {
            Matiere mat = _bddContext.Matieres.Where(m => m.Nom == matiere).FirstOrDefault();
            Niveau niv = _bddContext.Niveaux.Where(m => m.Nom == niveau).FirstOrDefault();
            Enseignant ens = new Enseignant();

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

        public Matiere ObtenirMatiere(int id)
        {
            return this._bddContext.Matieres.FirstOrDefault(c => c.Id == id);
        }

        public Niveau ObtenirNiveau(int id)
        {
            return this._bddContext.Niveaux.FirstOrDefault(c => c.Id == id);
        }
    }
}