using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Models
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();

        int CreerEleve(DateTime date, int utilisateurId);
        int CreerAdresse(int numeroRue, string rue, int codePostal, string ville);
        int CreerUtilisateur(string nom, string prenom, int adresseId);
        int CreerCompte(string identifiant, string password, int utilisateurId, int profilId, string role);
        int CreerProfil(string telephone, string image, string email);
        

        List<Utilisateur> ObtenirTousLesUtilisateurs();
        List<Compte> ObtenirTousLesComptes();
        List<Profil> ObtenirTousLesProfils();
        List<Adresse> ObtenirToutesLesAdresses();


        Compte Authentifier(string identifiant, string password);


        Compte ObtenirCompte(int id);
        Compte ObtenirCompte(string idStr);
        Utilisateur ObtenirUtilisateur(int id);
        Profil ObtenirProfil(int id);
        Adresse ObtenirAdresse(int id);

        Matiere ObtenirMatiere(int id);

        Niveau ObtenirNiveau(int id);
        Cours ObtenirCours(int id);

        void ModifierProfil(Profil profil);
        void ModifierCompte(Compte compte);
        void ModifierUtilisateur(Utilisateur utilisateur);
        void ModifierPassword(int id, string nouveauMDP);
        void ModifierAdresse(Adresse adresse);

        void SupprimerCours(Cours cours);
        void SupprimerCompte(Compte compte);
    }
}