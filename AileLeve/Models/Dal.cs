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

        public int CreerUtilisateur(string nom, string prenom)
        {
            Utilisateur utilisateur = new Utilisateur() { Nom = nom, Prenom = prenom };
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
                ProfilId = profilId
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
        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            return _bddContext.Utilisateurs.ToList();
        }
        public List<Compte> ObtenirTousLesComptes()
        {
            return _bddContext.Comptes.ToList();
        }
        public List<Profil> ObtenirTousLesProfils()
        {
            return _bddContext.Profils.ToList();
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

        public Compte Authentifier (string identifiant, string password){
            string motDePasse = EncodeMD5(password);
            Compte compte = this._bddContext.Comptes.FirstOrDefault(
                c=>c.Identifiant==identifiant && c.Password==motDePasse);
            return compte;            
        }
        public Compte ObtenirCompte(int id){
            return this._bddContext.Comptes.Find(id);
        }
        public Compte ObtenirCompte (string idStr){
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

    }
}