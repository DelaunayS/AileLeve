using AileLeve.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace AileleveTest
{
    //!! Les méthodes de tests sont groupées avec l'ordre alphabétique de leur classe Models
    public class UnitTest1
    {
        /* Tests sur la table Adresse
        */
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
        public void Modifier_Adresse_Verification()
        {
            {
                using (Dal dal = new Dal())
                {
                    dal.DeleteCreateDatabase();
                    int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                    Adresse adresse = dal.ObtenirAdresse(adresseId);
                    adresse.CodePostal = 44000;
                    adresse.Ville = "Nantes";
                    dal.ModifierAdresse(adresse);

                    List<Adresse> adresses = dal.ObtenirToutesLesAdresses();
                    Assert.NotNull(adresses);
                    Assert.Single(adresses);
                    Assert.Equal(5, adresses[0].NumeroRue);
                    Assert.Equal("rue des Roses", adresses[0].Rue);
                    Assert.Equal(44000, adresses[0].CodePostal);
                    Assert.Equal("Nantes", adresses[0].Ville);
                }
            }
        }

        [Fact]
        public void Supprimer_Adresse_Verification()
        {
            {
                using (Dal dal = new Dal())
                {
                    dal.DeleteCreateDatabase();
                    int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                    Adresse adresse = dal.ObtenirAdresse(adresseId);
                    dal.SupprimerAdresse(adresse);

                    List<Adresse> adresses = dal.ObtenirToutesLesAdresses();
                    Assert.Empty(adresses);
                }
            }
        }

        /* Tests sur la table Compte
        */
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
        public void Modifier_Compte_Verification()
        {
            {
                using (Dal dal = new Dal())
                {
                    dal.DeleteCreateDatabase();
                    int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                    int utilisateurId = dal.CreerUtilisateur("Rouget", "Lulu", adresseId);
                    int profilId = dal.CreerProfil("06 06 06 06 06", "SrcImage", "Lulu@gmail.com");
                    int compteId = dal.CreerCompte("RLulu", "rrrr", utilisateurId, profilId, "Eleve");
                    Compte compte = dal.ObtenirCompte(compteId);

                    compte.Utilisateur.Prenom = "Jean";
                    dal.ModifierCompte(compte);

                    List<Compte> comptes = dal.ObtenirTousLesComptes();
                    Assert.NotNull(comptes);
                    Assert.Single(comptes);
                    Assert.Equal("Jean", comptes[0].Utilisateur.Prenom);
                }
            }
        }


        /* Tests sur la table Eleve
        */
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
        public void Obtenir_Eleve_Par_Id_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                // eleve id=1
                int adresseId1 = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                int utilisateurId1 = dal.CreerUtilisateur("Rouget", "Lulu", adresseId1);
                DateTime dateNaissance1 = new DateTime(2012, 12, 22, 18, 00, 00);
                dal.CreerEleve(dateNaissance1, utilisateurId1);
                // eleve id=2
                int adresseId2 = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                int utilisateurId2 = dal.CreerUtilisateur("Rouget", "Lulu", adresseId2);
                DateTime dateNaissance2 = new DateTime(2012, 12, 22, 18, 00, 00);
                dal.CreerEleve(dateNaissance2, utilisateurId2);

                Eleve eleve = dal.ObtenirEleve(1);
                Assert.NotNull(eleve);
                Assert.Equal(1, eleve.Id);
            }
        }
        [Fact]
        public void Supprimer_Eleve_Par_Id_Verification()
        {
            {
                using (Dal dal = new Dal())
                {
                    dal.DeleteCreateDatabase();
                    // eleve id=1
                    int adresseId1 = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                    int utilisateurId1 = dal.CreerUtilisateur("Rouget", "Lulu", adresseId1);
                    DateTime dateNaissance1 = new DateTime(2012, 12, 22, 18, 00, 00);
                    dal.CreerEleve(dateNaissance1, utilisateurId1);
                    // eleve id=2
                    int adresseId2 = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                    int utilisateurId2 = dal.CreerUtilisateur("Rouget", "Lulu", adresseId2);
                    DateTime dateNaissance2 = new DateTime(2012, 12, 22, 18, 00, 00);
                    dal.CreerEleve(dateNaissance2, utilisateurId2);

                    dal.SupprimerEleveParId(2);
                    Assert.Null(dal.ObtenirEleve(2));
                }
            }
        }


        /* Tests sur la table EmploiDuTemps
        */
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

        /* Tests sur la table Enseignant
        */
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
        /* Tests sur la table Matiere
       */
        [Fact]
        public void Obtenir_Matiere_Par_Id_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.creerMatiere("Français");
                dal.creerMatiere("Mathématiques");
                dal.creerMatiere("Géographie");

                Matiere matiere = dal.ObtenirMatiere(2);
                Assert.NotNull(matiere);
                Assert.Equal("Mathématiques", matiere.Nom);
            }
        }

        /* Tests sur la table Niveau
       */
        [Fact]
        public void Obtenir_Niveau_Par_Id_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.creerNiveau("CP");
                dal.creerNiveau("CE1");
                dal.creerNiveau("CE2");

                Niveau niveau = dal.ObtenirNiveau(3);
                Assert.NotNull(niveau);
                Assert.Equal("CE2", niveau.Nom);
            }
        }

        /* Tests sur la table Notification
       */
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

        /* Tests sur la table Profil
       */
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
        public void Obtenir_Profil_Par_Id_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();                
                int profilId = dal.CreerProfil("06 06 06 06 06", "SrcImage", "Lulu@gmail.com");

                Assert.NotNull(dal.ObtenirProfil(profilId));
            }
        }
        [Fact]
        public void Supprimer_Profil_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int profilId = dal.CreerProfil("06 06 06 06 06", "SrcImage", "Lulu@gmail.com");
                Profil profil = dal.ObtenirProfil(profilId);
                dal.SupprimerProfil(profil);

                List<Profil> profils = dal.ObtenirTousLesProfils();
                Assert.Empty(profils);
            }
        }

        /* Tests sur la table RepresentantLegal
       */
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
                Utilisateur utilisateurRL=dal.ObtenirUtilisateur(utilisateurRLId);
                dal.CreerRepresentantLegal(utilisateurRL.Nom,utilisateurRL.Prenom, eleveId);

                List<RepresentantLegal> legals = dal.ObtenirTousLesRepresentantsLegaux();
                Assert.NotNull(legals);
                Assert.Single(legals);
                Assert.Equal(eleveId, legals[0].EleveId);
                Assert.Equal(utilisateurRL.Nom, legals[0].NomRL);
                 Assert.Equal(utilisateurRL.Prenom, legals[0].PrenomRL);
            }
        }

        /* Tests sur la table Utilisateurs
        */
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
        public void Obtenir_Utilisateur_Par_Id_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                int utilisateurId = dal.CreerUtilisateur("Rouget", "Lulu", adresseId);

                Assert.NotNull(dal.ObtenirUtilisateur(utilisateurId));
            }
        }


        [Fact]
        public void Supprimer_Utilisateur_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int adresseId = dal.CreerAdresse(5, "rue des Roses", 75000, "Paris");
                int utilisateurId = dal.CreerUtilisateur("Rouget", "Lulu", adresseId);

                Utilisateur utilisateur = dal.ObtenirUtilisateur(utilisateurId);
                dal.SupprimerUtilisateur(utilisateur);

                List<Utilisateur> utilisateurs = dal.ObtenirTousLesUtilisateurs();
                Assert.Empty(utilisateurs);
            }
        }
    }
}
