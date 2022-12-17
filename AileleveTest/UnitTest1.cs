using AileLeve.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace AileleveTest
{
    public class UnitTest1
    {
        [Fact]
        public void Creation_Adresse_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");

                List<Adresse> adresses = dal.ObtenirToutesLesAdresses();
                Assert.NotNull(adresses);
                Assert.Single(adresses);
                Assert.Equal(5, adresses[0].NumeroRue);
                Assert.Equal("rue des Roses", adresses[0].Rue);
                Assert.Equal(75000, adresses[0].CodePostal);
                Assert.Equal("Paris", adresses[0].Ville);
            }
        }

        [Fact]
        public void Creation_Profil_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.CreerProfil("06 06 06 06 06", "SrcImage", "Lulu@gmail.com");

                List<Profil> profils = dal.ObtenirTousLesProfils();
                Assert.NotNull(profils);
                Assert.Single(profils);
                Assert.Equal("06 06 06 06 06", profils[0].Telephone);
                Assert.Equal("SrcImage", profils[0].Image);
                Assert.Equal("Lulu@gmail.com", profils[0].Email);
            }
        }

        [Fact]
        public void Creation_Compte_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                int utilisateurId = dal.CreerUtilisateur("Rouget", "Lulu", adresseId);
                int profilId = dal.CreerProfil("06 06 06 06 06", "SrcImage", "Lulu@gmail.com");
                dal.CreerCompte("RLulu", "rrrr", utilisateurId, profilId, "Eleve");

                List<Compte> comptes = dal.ObtenirTousLesComptes();
                Assert.NotNull(comptes);
                Assert.Single(comptes);
                Assert.Equal("RLulu", comptes[0].Identifiant);
                Assert.Equal(Dal.EncodeMD5("rrrr"), comptes[0].Password);
                Assert.Equal(utilisateurId, comptes[0].UtilisateurId);
                Assert.Equal(1, comptes[0].ProfilId);
                Assert.Equal("Eleve", comptes[0].Role);
            }
        }

        [Fact]
        public void Creation_Utilisateur_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                dal.CreerUtilisateur("Rouget", "Lulu", adresseId);

                List<Utilisateur> utilisateurs = dal.ObtenirTousLesUtilisateurs();
                Assert.NotNull(utilisateurs);
                Assert.Single(utilisateurs);
                Assert.Equal("Rouget", utilisateurs[0].Nom);
                Assert.Equal("Lulu", utilisateurs[0].Prenom);
                Assert.Equal(adresseId, utilisateurs[0].AdresseId);
            }
        }

        [Fact]
        public void Creation_EmploiDuTemps_Verification()
        {
            using (Dal dal = new Dal())
            {
                DateTime date = new DateTime(2022, 12, 22, 18, 00, 00);
                dal.CreerEmploiDuTemps(date);

                List<EmploiDuTempsEnseignant> edts = dal.ObtenirTousLesEmploisDuTemps();
                Assert.NotNull(edts);
                Assert.Single(edts);
                Assert.Equal(date, edts[0].DateTime);
                Assert.True(edts[0].Disponible);
            }
        }
        [Fact]
        public void Creation_Notification_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.CreerNotification("Test notification");

                List<Notification> notifications = dal.ObtenirNotifications();
                Assert.NotNull(notifications);
                Assert.Single(notifications);
                Assert.Equal("Test notification", notifications[0].TypeNotification);
                Assert.False(notifications[0].Lu);
            }
        }
        [Fact]
        public void Creation_Enseignant_Verification()
        {
            using (Dal dal = new Dal())
            {

                dal.DeleteCreateDatabase();
                int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                int utilisateurId = dal.CreerUtilisateur("Rouget", "Lulu", adresseId);
                dal.CreerEnseignant(utilisateurId);

                List<Enseignant> enseignants = dal.ObtenirTousLesEnseignants();
                Assert.NotNull(enseignants);
                Assert.Single(enseignants);
                Assert.Equal(utilisateurId, enseignants[0].UtilisateurId);
            }
        }

        [Fact]
        public void Creation_Eleve_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                int utilisateurId = dal.CreerUtilisateur("Rouget", "Lulu", adresseId);
                DateTime dateNaissance = new DateTime(2012, 12, 22, 18, 00, 00);
                dal.CreerEleve(dateNaissance, utilisateurId);

                List<Eleve> eleves = dal.ObtenirTousLesEleves();
                Assert.NotNull(eleves);
                Assert.Single(eleves);
                Assert.Equal(dateNaissance, eleves[0].DateDeNaissance);
                Assert.Equal(utilisateurId, eleves[0].UtilisateurId);
            }
        }


        [Fact]
        public void Creation_RepresentantLegal_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                int utilisateurId = dal.CreerUtilisateur("Rouget", "Lulu", adresseId);
                DateTime dateNaissance = new DateTime(2012, 12, 22, 18, 00, 00);
                int eleveId = dal.CreerEleve(dateNaissance, utilisateurId);
                int utilisateurRLId = dal.CreerUtilisateur("Rouget", "Marcel", adresseId);
                dal.CreerRepresentantLegal(utilisateurRLId, eleveId);

                List<RepresentantLegal> legals = dal.ObtenirTousLesRepresentantsLegaux();
                Assert.NotNull(legals);
                Assert.Single(legals);
                Assert.Equal(eleveId, legals[0].EleveId);
                Assert.Equal(utilisateurRLId, legals[0].UtilisateurId);
            }
        }
    }
}
