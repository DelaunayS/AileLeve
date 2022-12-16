using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AileLeve.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Profil> Profils { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Eleve> Eleves { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Niveau> Niveaux { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Etudie> Etudie { get; set; }
        public DbSet<Cours> Cours { get; set; }

        public DbSet<Notification> Notifications { get; set; }


        public DbSet<EmploiDuTempsEnseignant> EmploiDuTempsEnseignants { get; set; }
        public DbSet<EstDisponible> EstDisponible { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etudie>().HasKey(e => new { e.CoursId, e.EleveId });
            modelBuilder.Entity<EstDisponible>().HasKey(e => new { e.EnseignantId, e.EmploiDuTempsEnseignantId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseMySql("server=localhost;user id=root;password=#Badaboum44;database=AileLeve");
        }
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            this.Matieres.AddRange(
                new Matiere { Id = 1, Nom = "Mathématiques" },
                new Matiere { Id = 2, Nom = "Français" },
                new Matiere { Id = 3, Nom = "Anglais" },
                new Matiere { Id = 4, Nom = "Espagnol" },
                new Matiere { Id = 5, Nom = "Histoire" },
                new Matiere { Id = 6, Nom = "Géographie" },
                new Matiere { Id = 7, Nom = "EMC" },
                new Matiere { Id = 8, Nom = "Physique" },
                new Matiere { Id = 9, Nom = "Chimie" },
                new Matiere { Id = 10, Nom = "SVT" },
                new Matiere { Id = 11, Nom = "Philosophie" },
                new Matiere { Id = 12, Nom = "Technologie/Informatique" },
                new Matiere { Id = 13, Nom = "Allemand" },
                new Matiere { Id = 14, Nom = "Méditation" },
                new Matiere { Id = 15, Nom = "Spe-SES" },
                new Matiere { Id = 16, Nom = "Spe-Humanités" },
                new Matiere { Id = 17, Nom = "Spe-Physique" },
                new Matiere { Id = 18, Nom = "Spe-SVT" },
                new Matiere { Id = 19, Nom = "Spe-Mathématiques" },
                new Matiere { Id = 20, Nom = "Spe-Géopolitique" },
                new Matiere { Id = 21, Nom = "Spe-Sciences politiques" },
                new Matiere { Id = 22, Nom = "Spe-Histoire-géographie" },
                new Matiere { Id = 23, Nom = "Spe-Culture de l'antiquité" },
                new Matiere { Id = 24, Nom = "Spe-Numérique" },
                new Matiere { Id = 25, Nom = "Spe-Langues étrangères" },
                new Matiere { Id = 26, Nom = "Spe-Sciences de l'ingénieur" },
                new Matiere { Id = 27, Nom = "Spe-Arts" }
                );

            this.Niveaux.AddRange(
                new Niveau { Id = 1, Nom = "CP" },
                new Niveau { Id = 2, Nom = "CE1" },
                new Niveau { Id = 3, Nom = "CE2" },
                new Niveau { Id = 4, Nom = "CM1" },
                new Niveau { Id = 5, Nom = "CM2" },
                new Niveau { Id = 6, Nom = "6ème" },
                new Niveau { Id = 7, Nom = "5ème" },
                new Niveau { Id = 8, Nom = "4ème" },
                new Niveau { Id = 9, Nom = "3ème" },
                new Niveau { Id = 10, Nom = "Seconde" },
                new Niveau { Id = 11, Nom = "Premiere" },
                new Niveau { Id = 12, Nom = "Terminale" }
                );

            this.Utilisateurs.AddRange(
                new Utilisateur { Id = 1, Nom = "Dupond", Prenom = "Jean", AdresseId = 1 },
                new Utilisateur { Id = 2, Nom = "Dupont", Prenom = "Yann", AdresseId = 2 },
                new Utilisateur { Id = 3, Nom = "Dylan", Prenom = "Bob", AdresseId = 3 },
                new Utilisateur { Id = 4, Nom = "Bibb", Prenom = "Justine" },
                new Utilisateur { Id = 5, Nom = "Durand", Prenom = "Pierre", AdresseId = 5 },
                new Utilisateur { Id = 6, Nom = "Frost", Prenom = "Robert", AdresseId = 6 },
                new Utilisateur { Id = 7, Nom = "Ruskin", Prenom = "John", AdresseId = 7 },
                new Utilisateur { Id = 8, Nom = "Bobin", Prenom = "Christian", AdresseId = 8 },
                new Utilisateur { Id = 9, Nom = "Proust", Prenom = "Marcel", AdresseId = 9 },
                new Utilisateur { Id = 10, Nom = "Maulpoix", Prenom = "Jean-Michel", AdresseId = 10 },
                new Utilisateur { Id = 11, Nom = "Admin", Prenom = "Bernard", AdresseId = 11 }
            ); ;
            this.Profils.AddRange(
                new Profil { Id = 1, Telephone = "0625522552", Image = "/img/profil.jpg", Email = "dupondjean@gmail.fr" },
                new Profil { Id = 2, Telephone = "0725752552", Image = "/img/profil.jpg", Email = "dupontyann@yahoo.fr" },
                new Profil { Id = 3, Telephone = "0635528550", Image = "/img/profil.jpg", Email = "dylanbob@gmail.fr" },
                new Profil { Id = 4, Telephone = "0600548552", Image = "/img/profil.jpg", Email = "bibbjustine@hotmail.fr" },
                new Profil { Id = 5, Telephone = "0607788908", Image = "/img/profil.jpg", Email = "pierre.durand@gmail.com" },
                new Profil { Id = 6, Telephone = "0607788908", Image = "/img/profil.jpg", Email = "robert.frost@gmail.com" },
                new Profil { Id = 7, Telephone = "0607788908", Image = "/img/profil.jpg", Email = "utilisateur.aileleve@gmail.com" },
                new Profil { Id = 8, Telephone = "0607788908", Image = "/img/profil.jpg", Email = "christian.bobin@gmail.com" },
                new Profil { Id = 9, Telephone = "0607788908", Image = "/img/profil.jpg", Email = "marcel.proust@gmail.com" },
                new Profil { Id = 10, Telephone = "0607788908", Image = "/img/profil.jpg", Email = "jeanmichel.maulpoix@gmail.com" },
                new Profil { Id = 11, Telephone = "0102030405", Image = "/img/profil.jpg", Email = "aileleve.soutienscolaire@gmail.com" }
            );
            this.Comptes.AddRange(

            new Compte { Id = 1, Identifiant = "DupondJ", Password = Dal.EncodeMD5("ddddd"), UtilisateurId = 1, ProfilId = 1, StatusActif=true, Role="Enseignant" },
                new Compte { Id = 2, Identifiant = "DupontY", Password = Dal.EncodeMD5("dydyd"), UtilisateurId = 2, ProfilId = 2, StatusActif=true, Role="Enseignant" },
                new Compte { Id = 3, Identifiant = "Bobby", Password = Dal.EncodeMD5("bbbbb"), UtilisateurId = 3, ProfilId = 3, StatusActif=true, Role="Enseignant"},
                new Compte { Id = 4, Identifiant = "Juju", Password = Dal.EncodeMD5("jjjjj"), UtilisateurId = 4, ProfilId = 4, StatusActif=true, Role="Enseignant"},
                new Compte { Id = 5, Identifiant = "Pierrot", Password = Dal.EncodeMD5("ppppp"), UtilisateurId = 5, ProfilId = 5, StatusActif=true, Role="Recrutement"},
                new Compte { Id = 6, Identifiant = "bob", Password = Dal.EncodeMD5("bbbbb"), UtilisateurId = 6, ProfilId = 6 , StatusActif=true, Role="Eleve"},
                new Compte { Id = 7, Identifiant = "Jack", Password = Dal.EncodeMD5("dydyd"), UtilisateurId = 7, ProfilId = 7 , StatusActif=true, Role="Eleve"},
                new Compte { Id = 8, Identifiant = "Chris", Password = Dal.EncodeMD5("bbbbb"), UtilisateurId = 8, ProfilId = 8 , StatusActif=false, Role="Eleve"},
                new Compte { Id = 9, Identifiant = "Marcelou", Password = Dal.EncodeMD5("mmmmm"), UtilisateurId = 9, ProfilId = 9 , StatusActif=true,Role="Eleve"},
                new Compte { Id = 10, Identifiant = "JeanMi", Password = Dal.EncodeMD5("jmjmjm"), UtilisateurId = 10, ProfilId = 10 , StatusActif=true,Role="Eleve"},
                new Compte { Id = 11, Identifiant = "Admin", Password = Dal.EncodeMD5("isika"), UtilisateurId = 11, ProfilId = 11 ,StatusActif=true,Role="Admin"}

            );

            this.Adresses.AddRange(

                new Adresse { Id = 1, Rue = "avenue des Roses", NumeroRue = 50, CodePostal = 38000, Ville = "Grenoble" },
                new Adresse { Id = 2, Rue = "chemin des marais", NumeroRue = 2, CodePostal = 51290, Ville = "Saint-Remy-en-Bouzemont-Saint-Genest-et-Isson" },
                new Adresse { Id = 3, Rue = "impasse de l'espoir", NumeroRue = 150, CodePostal = 67000, Ville = "Strasbourg" },
                new Adresse { Id = 5, Rue = "rue des pommiers en fleurs", NumeroRue = 8, CodePostal = 80132, Ville = " Vauchelles-les-Quesnoy" },
                new Adresse { Id = 6, Rue = "rue des neiges éternelles", NumeroRue = 1, CodePostal = 04000, Ville = "Digne-Les-Bains" },
                new Adresse { Id = 7, Rue = "rue de l'instant suspendu", NumeroRue = 5, CodePostal = 75001, Ville = "Paris" },
                new Adresse { Id = 8, Rue = "chemin de l'école buissonnière", NumeroRue = 27, CodePostal = 20169, Ville = "Bonifacio" },
                new Adresse { Id = 9, Rue = "allée du côté de chez Swann", NumeroRue = 43, CodePostal = 28120, Ville = "'Illiers-Combray. " },
                new Adresse { Id = 10, Rue = "rue de la pointe du jour", NumeroRue = 17, CodePostal = 92100, Ville = " Boulogne" },
                new Adresse { Id = 11, Rue = "Rue Danton", NumeroRue = 3, CodePostal = 92240, Ville = " Malakoff" }
                );

            this.Enseignants.AddRange(
                new Enseignant { Id = 1, UtilisateurId = 1 },
                new Enseignant { Id = 2, UtilisateurId = 2 },
                new Enseignant { Id = 3, UtilisateurId = 3 },
                new Enseignant { Id = 4, UtilisateurId = 4 },
                new Enseignant { Id = 5, UtilisateurId = 5 }
                );



            this.Eleves.AddRange(
                new Eleve { Id = 1, DateDeNaissance = new DateTime(2006, 08, 15), UtilisateurId = 6 },
                new Eleve { Id = 2, DateDeNaissance = new DateTime(2013, 12, 18), UtilisateurId = 7 },
                new Eleve { Id = 3, DateDeNaissance = new DateTime(2015, 07, 15), UtilisateurId = 8 },
                new Eleve { Id = 4, DateDeNaissance = new DateTime(2004, 02, 14), UtilisateurId = 9 },
                new Eleve { Id = 5, DateDeNaissance = new DateTime(2006, 03, 08), UtilisateurId = 10 }
                );

            this.Cours.AddRange(

                new Cours { Id = 1, MatiereId = 1, NiveauId = 1, EnseignantId = 1, TypeCours = TypeCours.domicile },
                new Cours { Id = 2, MatiereId = 2, NiveauId = 2, EnseignantId = 2, TypeCours = TypeCours.synchrone },
                new Cours { Id = 3, MatiereId = 3, NiveauId = 3, EnseignantId = 3, TypeCours = TypeCours.domicile },
                new Cours { Id = 4, MatiereId = 4, NiveauId = 4, EnseignantId = 4, TypeCours = TypeCours.synchrone },
                new Cours { Id = 5, MatiereId = 5, NiveauId = 5, EnseignantId = 5, TypeCours = TypeCours.domicile },
                new Cours { Id = 6, MatiereId = 6, NiveauId = 6, EnseignantId = 1, TypeCours = TypeCours.synchrone },
                new Cours { Id = 7, MatiereId = 7, NiveauId = 7, EnseignantId = 2, TypeCours = TypeCours.domicile },
                new Cours { Id = 8, MatiereId = 8, NiveauId = 8, EnseignantId = 3, TypeCours = TypeCours.synchrone },
                new Cours { Id = 9, MatiereId = 9, NiveauId = 9, EnseignantId = 4, TypeCours = TypeCours.domicile },
                new Cours { Id = 10, MatiereId = 10, NiveauId = 1, EnseignantId = 5, TypeCours = TypeCours.synchrone },
                new Cours { Id = 11, MatiereId = 11, NiveauId = 2, EnseignantId = 1, TypeCours = TypeCours.domicile },
                new Cours { Id = 12, MatiereId = 12, NiveauId = 3, EnseignantId = 2, TypeCours = TypeCours.synchrone },
                new Cours { Id = 13, MatiereId = 13, NiveauId = 4, EnseignantId = 3, TypeCours = TypeCours.domicile },
                new Cours { Id = 14, MatiereId = 14, NiveauId = 5, EnseignantId = 4, TypeCours = TypeCours.synchrone },
                new Cours { Id = 15, MatiereId = 15, NiveauId = 10, EnseignantId = 5, TypeCours = TypeCours.domicile },
                new Cours { Id = 16, MatiereId = 16, NiveauId = 11, EnseignantId = 1, TypeCours = TypeCours.synchrone },
                new Cours { Id = 17, MatiereId = 17, NiveauId = 12, EnseignantId = 2, TypeCours = TypeCours.domicile },
                new Cours { Id = 18, MatiereId = 18, NiveauId = 10, EnseignantId = 3, TypeCours = TypeCours.synchrone },
                new Cours { Id = 19, MatiereId = 19, NiveauId = 11, EnseignantId = 4, TypeCours = TypeCours.domicile },
                new Cours { Id = 20, MatiereId = 20, NiveauId = 12, EnseignantId = 5, TypeCours = TypeCours.synchrone }


            );


            this.Etudie.AddRange(
                 new Etudie { EleveId = 1, CoursId = 12 },
                 new Etudie { EleveId = 2, CoursId = 5 },
                 new Etudie { EleveId = 3, CoursId = 2 },
                 new Etudie { EleveId = 4, CoursId = 10 },
                 new Etudie { EleveId = 5, CoursId = 11 },
                 new Etudie { EleveId = 1, CoursId = 4 },
                 new Etudie { EleveId = 2, CoursId = 1 },
                 new Etudie { EleveId = 3, CoursId = 4 },
                 new Etudie { EleveId = 4, CoursId = 14 },
                 new Etudie { EleveId = 5, CoursId = 18 },
                 new Etudie { EleveId = 1, CoursId = 15 },
                 new Etudie { EleveId = 2, CoursId = 8 },
                 new Etudie { EleveId = 3, CoursId = 9 },
                 new Etudie { EleveId = 4, CoursId = 5 },
                 new Etudie { EleveId = 5, CoursId = 1 },
                 new Etudie { EleveId = 1, CoursId = 14 },
                 new Etudie { EleveId = 2, CoursId = 18 },
                 new Etudie { EleveId = 3, CoursId = 3 },
                  new Etudie { EleveId = 4, CoursId = 13 },
                  new Etudie { EleveId = 5, CoursId = 19 }

                 );


            this.EmploiDuTempsEnseignants.AddRange(
           new EmploiDuTempsEnseignant { Id = 1, DateTime = new DateTime(2022, 12, 21, 10, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 2, DateTime = new DateTime(2022, 12, 22, 11, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 3, DateTime = new DateTime(2022, 12, 23, 10, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 4, DateTime = new DateTime(2022, 12, 24, 10, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 5, DateTime = new DateTime(2023, 01, 05, 09, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 6, DateTime = new DateTime(2023, 01, 06, 10, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 7, DateTime = new DateTime(2023, 01, 07, 11, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 8, DateTime = new DateTime(2023, 01, 08, 14, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 9, DateTime = new DateTime(2023, 01, 12, 10, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 10, DateTime = new DateTime(2023, 01, 09, 10, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 11, DateTime = new DateTime(2023, 01, 10, 11, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 12, DateTime = new DateTime(2023, 01, 11, 10, 00, 00), Disponible = true }

           );

            this.EstDisponible.AddRange(
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 1 },
            new EstDisponible { EnseignantId = 2, EmploiDuTempsEnseignantId = 2 },
            new EstDisponible { EnseignantId = 3, EmploiDuTempsEnseignantId = 3 },
            new EstDisponible { EnseignantId = 4, EmploiDuTempsEnseignantId = 4 },
            new EstDisponible { EnseignantId = 5, EmploiDuTempsEnseignantId = 5 },
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 8},
            new EstDisponible { EnseignantId = 2, EmploiDuTempsEnseignantId = 6 },
            new EstDisponible { EnseignantId = 3, EmploiDuTempsEnseignantId = 7 },
            new EstDisponible { EnseignantId = 3, EmploiDuTempsEnseignantId = 1 },
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 5 }
            );

            this.Notifications.AddRange(
                new Notification { Id = 1, Lu = true, TypeNotification = "L'élève Proust Marcel s'est inscrite sur la plateforme le 14/12/2022 14:31" },
                new Notification { Id = 2, Lu = false, TypeNotification = "L'enseignant Dupond Jean s'est inscrit sur la plateforme le 13/12/2022 18:42" }
  
            );
            

            this.SaveChanges();




        }



    }
}