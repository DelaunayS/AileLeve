using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Models
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();
        int CreerUtilisateur (string nom, string prenom);
        int CreerCompte (string identifiant, string password, int utilisateurId, int profilId);
        int CreerProfil (string telephone, string image, string email);
        
        List<Utilisateur> ObtenirTousLesUtilisateurs(); 
        List<Compte> ObtenirTousLesComptes(); 
        List<Profil> ObtenirTousLesProfils(); 

        Compte Authentifier (string identifiant, string password);
        
        Compte ObtenirCompte (int id);
        Compte ObtenirCompte (string idStr);
        Utilisateur ObtenirUtilisateur(int id);
        Profil ObtenirProfil(int id);
        
        void ModifierProfil(int id, string telephone, string mail, string image);
        void ModifierCompte(Compte compte);
        void ModifierUtilisateur(Utilisateur utilisateur);
    }
}