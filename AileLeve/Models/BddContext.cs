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

        public DbSet<RepresentantLegal> RepresentantLegaux{ get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etudie>().HasKey(e => new { e.CoursId, e.EleveId });
            modelBuilder.Entity<EstDisponible>().HasKey(e => new { e.EnseignantId, e.EmploiDuTempsEnseignantId, e.CoursId });
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
                new Matiere { Id = 21, Nom = "Spe-Sciences-politiques" },
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
                new Niveau { Id = 12, Nom = "Terminale" },
                new Niveau { Id = 13, Nom = "Tous niveaux" }
                );

            this.Utilisateurs.AddRange(
                new Utilisateur { Id = 1, Nom = "Martin", Prenom = "Céline", AdresseId = 1 },
                new Utilisateur { Id = 2, Nom = "Dupont", Prenom = "Yann"},
                new Utilisateur { Id = 3, Nom = "Dylan", Prenom = "Ben", AdresseId = 3 },
                new Utilisateur { Id = 4, Nom = "Bibb", Prenom = "Justine" },
                new Utilisateur { Id = 5, Nom = "Durand", Prenom = "Pierre", AdresseId = 5 },
                new Utilisateur { Id = 6, Nom = "Frost", Prenom = "Robert", AdresseId = 6 },
                new Utilisateur { Id = 7, Nom = "Ruskin", Prenom = "John", AdresseId = 7 },
                new Utilisateur { Id = 8, Nom = "Bobin", Prenom = "Christian", AdresseId = 8 },
                new Utilisateur { Id = 9, Nom = "Proust", Prenom = "Marcel", AdresseId = 2 },
                new Utilisateur { Id = 10, Nom = "Maulpoix", Prenom = "Jean-Michel", AdresseId = 10 },
                new Utilisateur { Id = 11, Nom = "Admin", Prenom = "Bernard", AdresseId = 11 },
                new Utilisateur { Id = 12, Nom = "Duras", Prenom = "Marguerite", AdresseId = 9 },
                new Utilisateur { Id = 13, Nom = "Gave", Prenom = "Charles", AdresseId = 12 },
                new Utilisateur { Id = 14, Nom = "Onfray", Prenom = "Michel", AdresseId = 10 },
                new Utilisateur { Id = 15, Nom = "Delamarche", Prenom = "Olivier", AdresseId = 13 },
                new Utilisateur { Id = 16, Nom = "Veil", Prenom = "Simone", AdresseId = 14 },
                new Utilisateur { Id = 17, Nom = "Houellebecq", Prenom = "Michel", AdresseId = 15 },
                new Utilisateur { Id = 18, Nom = "Finkelkraut", Prenom = "Alain", AdresseId = 16 },
                new Utilisateur { Id = 19, Nom = "Bernard", Prenom = "Claude" },
                new Utilisateur { Id = 20, Nom = "Moulin", Prenom = "Jean", AdresseId = 4 }
            ); ;


           this.Profils.AddRange(
               new Profil { Id = 1, Telephone = "0625522552", Image = "/img/BibbJustine.jpg", Email = "celinemartin@gmail.com" },
                new Profil { Id = 2, Telephone = "0725752552", Image = "/img/DupontYann.png", Email = "dupontyann@yahoo.fr" },
                new Profil { Id = 3, Telephone = "0635528550", Image = "/img/DylanBob.jpg", Email = "dylanbob@gmail.fr" },
                new Profil { Id = 4, Telephone = "0600548552", Image = "/img/BibbJustine.jpg", Email = "bibbjustine@hotmail.fr" },
                new Profil { Id = 5, Telephone = "0607788908", Image = "/img/DurandPierre.jpg", Email = "pierre.durand@gmail.com" },
                new Profil { Id = 6, Telephone = "0607788908", Image = "/img/FrostRobert.png", Email = "robert.frost@gmail.com" },
                new Profil { Id = 7, Telephone = "0607788908", Image = "/img/RuskinJohn.jpg", Email = "utilisateur.aileleve@gmail.com" },
                new Profil { Id = 8, Telephone = "0607788908", Image = "/img/profil.jpg", Email = "christian.bobin@gmail.com" },
                new Profil { Id = 9, Telephone = "0607788908", Image = "/img/ProustMarcel.jpg", Email = "marcel.proust@gmail.com" },
                new Profil { Id = 10, Telephone = "0607788908", Image = "/img/MaulpoixJM.jpg", Email = "jeanmichel.maulpoix@gmail.com" },
                new Profil { Id = 11, Telephone = "0102030405", Image = "/img/Aile'lèveV3.png", Email = "aileleve.soutienscolaire@gmail.com" },
                new Profil { Id = 12, Telephone = "0647859636", Image = "/img/profil.jpg", Email = "margot-duras@gmail.com" },
                new Profil { Id = 13, Image = "/img/profil.jpg", Email = "gave.c@gmail.com" },
                new Profil { Id = 14, Telephone = "0647859636", Image = "/img/profil.jpg", Email = "onfray@gmail.com" },
                new Profil { Id = 15, Telephone = "0478965899", Image = "/img/profil.jpg", Email = "olive.eco@gmail.com" },
                new Profil { Id = 16, Telephone = "0649369985", Image = "/img/profil.jpg", Email = "veil@gmail.com" },
                new Profil { Id = 17, Telephone = "0647859636", Image = "/img/profil.jpg", Email = "hou.mich@gmail.com" },
                new Profil { Id = 18, Telephone = "0639598989", Image = "/img/profil.jpg", Email = "alain.f@gmail.com" },
                new Profil { Id = 19, Email = "b.claudio@gmail.com" },
                new Profil { Id = 20, Telephone = "0418759899", Image = "/img/profil.jpg", Email = "moulin.j@gmail.com" }
            );

            this.Comptes.AddRange(
            new Compte { Id = 1, Identifiant = "CelineM", Password = Dal.EncodeMD5("ddddd"), UtilisateurId = 1, ProfilId = 1, StatusActif=true, Role="Enseignant" },
                new Compte { Id = 2, Identifiant = "DupontY", Password = Dal.EncodeMD5("dydyd"), UtilisateurId = 2, ProfilId = 2, StatusActif=true, Role="Enseignant" },
                new Compte { Id = 3, Identifiant = "Bobby", Password = Dal.EncodeMD5("bbbbb"), UtilisateurId = 3, ProfilId = 3, StatusActif=true, Role="Enseignant"},
                new Compte { Id = 4, Identifiant = "Juju", Password = Dal.EncodeMD5("jjjjj"), UtilisateurId = 4, ProfilId = 4, StatusActif=true, Role="Enseignant"},
                new Compte { Id = 5, Identifiant = "Pierrot", Password = Dal.EncodeMD5("ppppp"), UtilisateurId = 5, ProfilId = 5, StatusActif=true, Role="Recrutement"},
                new Compte { Id = 6, Identifiant = "RobFrost", Password = Dal.EncodeMD5("bbbbb"), UtilisateurId = 6, ProfilId = 6 , StatusActif=true, Role="Eleve"},
                new Compte { Id = 7, Identifiant = "Rusk", Password = Dal.EncodeMD5("dydyd"), UtilisateurId = 7, ProfilId = 7 , StatusActif=true, Role="Eleve"},
                new Compte { Id = 8, Identifiant = "ChrisBob", Password = Dal.EncodeMD5("bbbbb"), UtilisateurId = 8, ProfilId = 8 , StatusActif=false, Role="Eleve"},
                new Compte { Id = 9, Identifiant = "Marcelou", Password = Dal.EncodeMD5("mmmmm"), UtilisateurId = 9, ProfilId = 9 , StatusActif=true,Role="Eleve"},
                new Compte { Id = 10, Identifiant = "JeanMi", Password = Dal.EncodeMD5("jmjmjm"), UtilisateurId = 10, ProfilId = 10 , StatusActif=true,Role="Eleve"},
                new Compte { Id = 11, Identifiant = "Admin", Password = Dal.EncodeMD5("isika"), UtilisateurId = 11, ProfilId = 11 ,StatusActif=true,Role="Admin"},
                new Compte { Id = 12, Identifiant = "MargotD", Password = Dal.EncodeMD5("mmmmmd"), UtilisateurId = 12, ProfilId = 12 ,StatusActif=true,Role= "Eleve" },
                new Compte { Id = 13, Identifiant = "Gavekal", Password = Dal.EncodeMD5("ggggg"), UtilisateurId = 13, ProfilId = 13, StatusActif = true, Role = "Recrutement" },
                new Compte { Id = 14, Identifiant = "Michou", Password = Dal.EncodeMD5("mmmmi"), UtilisateurId = 14, ProfilId = 14, StatusActif = true, Role = "Recrutement" },
                new Compte { Id = 15, Identifiant = "Olive", Password = Dal.EncodeMD5("ololol"), UtilisateurId = 15, ProfilId = 15, StatusActif = true, Role = "Enseignant" },
                new Compte { Id = 16, Identifiant = "SimVeil", Password = Dal.EncodeMD5("vvvvv"), UtilisateurId = 16, ProfilId = 16, StatusActif = true, Role = "Eleve" },
                new Compte { Id = 17, Identifiant = "HouMich", Password = Dal.EncodeMD5("michmich"), UtilisateurId = 17, ProfilId = 17, StatusActif = true, Role = "Enseignant" },
                new Compte { Id = 18, Identifiant = "AlainF", Password = Dal.EncodeMD5("fffff"), UtilisateurId = 18, ProfilId = 18, StatusActif = true, Role = "Eleve" },
                new Compte { Id = 19, Identifiant = "Bernardo", Password = Dal.EncodeMD5("bbbbbbr"), UtilisateurId = 19, ProfilId = 19, StatusActif = true, Role = "Enseignant" },
                new Compte { Id = 20, Identifiant = "MoulinJ", Password = Dal.EncodeMD5("mmmmmj"), UtilisateurId = 20, ProfilId = 20, StatusActif = true, Role = "Eleve" }

            );

            this.Adresses.AddRange(
                new Adresse { Id = 1, Rue = "avenue des Roses", NumeroRue = 50, CodePostal = 38000, Ville = "Grenoble" },
                new Adresse { Id = 2, Rue = "chemin des marais", NumeroRue = 2, CodePostal = 51290, Ville = "Saint-Remy-en-Bouzemont-Saint-Genest-et-Isson" },
                new Adresse { Id = 3, Rue = "impasse de l'espoir", NumeroRue = 150, CodePostal = 67000, Ville = "Strasbourg" },
                new Adresse { Id = 4, Rue = "rue des roses blanches", NumeroRue = 48, CodePostal = 34000, Ville = " Bordeaux" },
                new Adresse { Id = 5, Rue = "rue des pommiers en fleurs", NumeroRue = 8, CodePostal = 80132, Ville = " Vauchelles-les-Quesnoy" },
                new Adresse { Id = 6, Rue = "rue des neiges éternelles", NumeroRue = 1, CodePostal = 04000, Ville = "Digne-Les-Bains" },
                new Adresse { Id = 7, Rue = "rue de l'instant suspendu", NumeroRue = 5, CodePostal = 75001, Ville = "Paris" },
                new Adresse { Id = 8, Rue = "chemin de l'école buissonnière", NumeroRue = 27, CodePostal = 20169, Ville = "Bonifacio" },
                new Adresse { Id = 9, Rue = "allée du côté de chez Swann", NumeroRue = 43, CodePostal = 28120, Ville = " Illiers-Combray" },
                new Adresse { Id = 10, Rue = "rue de la pointe du jour", NumeroRue = 17, CodePostal = 92100, Ville = " Boulogne" },
                new Adresse { Id = 11, Rue = "rue Danton", NumeroRue = 3, CodePostal = 92240, Ville = " Malakoff" },
                new Adresse { Id = 12, Rue = "boulevard des Belges", NumeroRue = 8, CodePostal = 01000, Ville = " Ain" },
                new Adresse { Id = 13, Rue = "rond-point de l'Europe", NumeroRue = 142, CodePostal = 67000, Ville = " Strasbourg" },
                new Adresse { Id = 14, Rue = "chemin du Lac des sapins", NumeroRue = 80, CodePostal = 74000, Ville = " Annecy" },
                new Adresse { Id = 15, Rue = "avenue du Soleil", NumeroRue = 5, CodePostal = 31000, Ville = " Toulouse" },
                new Adresse { Id = 16, Rue = "chemin du Lagon", NumeroRue = 5, CodePostal = 13000, Ville = " Marseille" }
        );

            this.Enseignants.AddRange(
                new Enseignant { Id = 1, UtilisateurId = 1 },
                new Enseignant { Id = 2, UtilisateurId = 2 },
                new Enseignant { Id = 3, UtilisateurId = 3 },
                new Enseignant { Id = 4, UtilisateurId = 4 },
                new Enseignant { Id = 5, UtilisateurId = 17 },
                new Enseignant { Id = 6, UtilisateurId = 19 },
                new Enseignant { Id = 7, UtilisateurId = 15 }
                );

            this.Eleves.AddRange(
                new Eleve { Id = 1, DateDeNaissance = new DateTime(2014, 08, 15), UtilisateurId = 6 },
                new Eleve { Id = 2, DateDeNaissance = new DateTime(2011, 12, 18), UtilisateurId = 7 },
                new Eleve { Id = 3, DateDeNaissance = new DateTime(2004, 07, 15), UtilisateurId = 8 },
                new Eleve { Id = 4, DateDeNaissance = new DateTime(2006, 02, 14), UtilisateurId = 9 },
                new Eleve { Id = 5, DateDeNaissance = new DateTime(2015, 03, 08), UtilisateurId = 10 },
                new Eleve { Id = 6, DateDeNaissance = new DateTime(2007, 05, 28), UtilisateurId = 12 },
                new Eleve { Id = 7, DateDeNaissance = new DateTime(2007, 04, 06), UtilisateurId = 20},
                new Eleve { Id = 8, DateDeNaissance = new DateTime(2009, 05, 26), UtilisateurId = 16 },
                new Eleve { Id = 9, DateDeNaissance = new DateTime(2003, 09, 14), UtilisateurId = 18 }
                );


            this.RepresentantLegaux.AddRange(
    new RepresentantLegal { Id = 1, NomRL = "Prévert", PrenomRL = "Jacques", EleveId = 1 },
    new RepresentantLegal { Id = 2, NomRL = "Hugo", PrenomRL = "Victor", EleveId = 3 },
    new RepresentantLegal { Id = 3, NomRL = "Baudelaire", PrenomRL = "Charles", EleveId = 5 },
    new RepresentantLegal { Id = 4, NomRL = "Desnos", PrenomRL = "Robert", EleveId = 7 },
    new RepresentantLegal { Id = 5, NomRL = "De la Fortelle", PrenomRL = "Guy", EleveId = 9 },
    new RepresentantLegal { Id = 6, NomRL = "Obertone", PrenomRL = "Laurent", EleveId = 2 },
    new RepresentantLegal { Id = 7, NomRL = "Valéry", PrenomRL = "Paul", EleveId = 4 },
    new RepresentantLegal { Id = 8, NomRL = "Péguy", PrenomRL = "Charles", EleveId = 6 },
    new RepresentantLegal { Id = 9, NomRL = "Murer", PrenomRL = "Philippe", EleveId = 8 }
    );

      


            this.Cours.AddRange(

                new Cours { Id = 1, MatiereId = 1, NiveauId = 1, EnseignantId = 1, TypeCours = TypeCours.domicile },
                new Cours { Id = 2, MatiereId = 2, NiveauId = 1, EnseignantId = 1, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 3, MatiereId = 3, NiveauId = 1, EnseignantId = 1, TypeCours = TypeCours.domicile },
                new Cours { Id = 4, MatiereId = 5, NiveauId = 1, EnseignantId = 1, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 5, MatiereId = 1, NiveauId = 1, EnseignantId = 1, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 6, MatiereId = 3, NiveauId = 2, EnseignantId = 1, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 7, MatiereId = 11, NiveauId = 12, EnseignantId = 7, TypeCours = TypeCours.onlineSynchrone },

                new Cours { Id = 8, MatiereId = 2, NiveauId = 2, EnseignantId = 2, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 9, MatiereId = 2, NiveauId = 5, EnseignantId = 2, TypeCours = TypeCours.domicile },
                new Cours { Id = 10, MatiereId = 3, NiveauId = 5, EnseignantId = 2, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 11, MatiereId = 3, NiveauId = 3, EnseignantId = 2, TypeCours = TypeCours.domicile },

                new Cours { Id = 12, MatiereId = 3, NiveauId = 3, EnseignantId = 3, TypeCours = TypeCours.domicile },
                new Cours { Id = 13, MatiereId = 6, NiveauId = 3, EnseignantId = 3, TypeCours = TypeCours.domicile },
                new Cours { Id = 14, MatiereId = 1, NiveauId = 2, EnseignantId = 3, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 15, MatiereId = 2, NiveauId = 2, EnseignantId = 3, TypeCours = TypeCours.onlineSynchrone },

                new Cours { Id = 16, MatiereId = 2, NiveauId = 8, EnseignantId = 5, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 17, MatiereId = 5, NiveauId = 8, EnseignantId = 5, TypeCours = TypeCours.domicile },
                new Cours { Id = 18, MatiereId = 2, NiveauId = 9, EnseignantId = 5, TypeCours = TypeCours.domicile },
                new Cours { Id = 19, MatiereId = 5, NiveauId = 9, EnseignantId = 5, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 20, MatiereId = 23, NiveauId = 9, EnseignantId = 5, TypeCours = TypeCours.onlineSynchrone },
               
                new Cours { Id = 21, MatiereId = 21, NiveauId = 12, EnseignantId = 7, TypeCours = TypeCours.domicile },
                new Cours { Id = 22, MatiereId = 15, NiveauId = 10, EnseignantId = 7, TypeCours = TypeCours.domicile },
                new Cours { Id = 23, MatiereId = 5, NiveauId = 12, EnseignantId = 7, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 24, MatiereId = 6, NiveauId = 12, EnseignantId = 7, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 25, MatiereId = 20, NiveauId = 12, EnseignantId = 7, TypeCours = TypeCours.onlineSynchrone },

                new Cours { Id = 26, MatiereId = 8, NiveauId = 9, EnseignantId = 4, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 27, MatiereId = 2, NiveauId = 8, EnseignantId = 4, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 28, MatiereId = 10, NiveauId = 7, EnseignantId = 4, TypeCours = TypeCours.onlineSynchrone },

                new Cours { Id = 29, MatiereId = 11, NiveauId = 10, EnseignantId = 6, TypeCours = TypeCours.onlineSynchrone },
                new Cours { Id = 30, MatiereId = 11, NiveauId = 12, EnseignantId = 6, TypeCours = TypeCours.onlineSynchrone }
            );
                                                        
            this.Etudie.AddRange(

                new Etudie { EleveId = 1, CoursId = 6},
                new Etudie { EleveId = 1, CoursId = 15 },
                new Etudie { EleveId = 1, CoursId = 8 },
                new Etudie { EleveId = 1, CoursId = 14 },
                
                new Etudie { EleveId = 2, CoursId = 9 },
                new Etudie { EleveId = 2, CoursId = 10 },
           
                new Etudie { EleveId = 3, CoursId = 30 },
                new Etudie { EleveId = 3, CoursId = 25 },
                new Etudie { EleveId = 3, CoursId = 24},
                new Etudie { EleveId = 3, CoursId = 23},
                new Etudie { EleveId = 3, CoursId = 21 },
                new Etudie { EleveId = 3, CoursId = 7 },

                new Etudie { EleveId = 4, CoursId = 22 },
                new Etudie { EleveId = 4, CoursId = 29 },
                
                new Etudie { EleveId = 5, CoursId = 1 },
                new Etudie { EleveId = 5, CoursId = 4 },
                new Etudie { EleveId = 5, CoursId = 3 },
                new Etudie { EleveId = 5, CoursId = 2},
                new Etudie { EleveId = 5, CoursId = 5 },
        
                new Etudie { EleveId = 6, CoursId = 16},
                new Etudie { EleveId = 6, CoursId = 17},

                new Etudie { EleveId = 7, CoursId = 20},
                new Etudie { EleveId = 7, CoursId = 19},
                new Etudie { EleveId = 7, CoursId = 18},

                new Etudie { EleveId = 8, CoursId = 11}, 
                new Etudie { EleveId = 8, CoursId = 13 } 
                );


            this.EmploiDuTempsEnseignants.AddRange(
           new EmploiDuTempsEnseignant { Id = 1, DateTime = new DateTime(2022, 12, 21, 16, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 2, DateTime = new DateTime(2022, 12, 22, 18, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 3, DateTime = new DateTime(2022, 12, 23, 17, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 4, DateTime = new DateTime(2022, 12, 24, 18, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 5, DateTime = new DateTime(2023, 01, 05, 16, 30, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 6, DateTime = new DateTime(2023, 01, 06, 14, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 7, DateTime = new DateTime(2023, 01, 07, 11, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 8, DateTime = new DateTime(2023, 01, 09, 18, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 9, DateTime = new DateTime(2023, 01, 10, 19, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 10, DateTime = new DateTime(2023, 01, 11, 15, 00, 00), Disponible = false },
           new EmploiDuTempsEnseignant { Id = 11, DateTime = new DateTime(2023, 01, 12, 18, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 12, DateTime = new DateTime(2023, 01, 13, 18, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 13, DateTime = new DateTime(2023, 01, 14, 11, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 14, DateTime = new DateTime(2023, 01, 16, 16, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 15, DateTime = new DateTime(2023, 01, 17, 17, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 16, DateTime = new DateTime(2023, 01, 18, 17, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 17, DateTime = new DateTime(2023, 01, 19, 18, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 18, DateTime = new DateTime(2023, 01, 20, 16, 30, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 19, DateTime = new DateTime(2023, 01, 21, 10, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 20, DateTime = new DateTime(2023, 01, 23, 16, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 21, DateTime = new DateTime(2023, 01, 24, 18, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 22, DateTime = new DateTime(2023, 01, 25, 19, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 23, DateTime = new DateTime(2023, 01, 26, 15, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 24, DateTime = new DateTime(2023, 01, 27, 18, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 25, DateTime = new DateTime(2023, 01, 28, 14, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 26, DateTime = new DateTime(2023, 01, 30, 18, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 27, DateTime = new DateTime(2023, 01, 31, 17, 30, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 28, DateTime = new DateTime(2023, 02, 01, 18, 00, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 29, DateTime = new DateTime(2023, 02, 02, 18, 30, 00), Disponible = true },
           new EmploiDuTempsEnseignant { Id = 30, DateTime = new DateTime(2023, 02, 03, 17, 45, 00), Disponible = true }
           );
          


            this.EstDisponible.AddRange(
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 1, CoursId = 1 },
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 3, CoursId = 2 },
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 5, CoursId = 3 },
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 6, CoursId = 4 },
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 9, CoursId = 5 },
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 11, CoursId = 6 },
            new EstDisponible { EnseignantId = 1, EmploiDuTempsEnseignantId = 13, CoursId = 7 },

            new EstDisponible { EnseignantId = 2, EmploiDuTempsEnseignantId = 2, CoursId = 8 },
            new EstDisponible { EnseignantId = 2, EmploiDuTempsEnseignantId = 7, CoursId = 9 },
            new EstDisponible { EnseignantId = 2, EmploiDuTempsEnseignantId = 14, CoursId = 10 },
            new EstDisponible { EnseignantId = 2, EmploiDuTempsEnseignantId = 10, CoursId = 11 },

            new EstDisponible { EnseignantId = 3, EmploiDuTempsEnseignantId = 4, CoursId = 12 },
            new EstDisponible { EnseignantId = 3, EmploiDuTempsEnseignantId = 12, CoursId = 13 },
            new EstDisponible { EnseignantId = 3, EmploiDuTempsEnseignantId = 16, CoursId = 14 },
            new EstDisponible { EnseignantId = 3, EmploiDuTempsEnseignantId = 20, CoursId = 15 },

            new EstDisponible { EnseignantId = 4, EmploiDuTempsEnseignantId = 8, CoursId = 26 },
            new EstDisponible { EnseignantId = 4, EmploiDuTempsEnseignantId = 15, CoursId = 27 },
            new EstDisponible { EnseignantId = 4, EmploiDuTempsEnseignantId = 23, CoursId = 28 },

            new EstDisponible { EnseignantId = 5, EmploiDuTempsEnseignantId = 17, CoursId = 16 },
            new EstDisponible { EnseignantId = 5, EmploiDuTempsEnseignantId = 19, CoursId = 17 },
            new EstDisponible { EnseignantId = 5, EmploiDuTempsEnseignantId = 18, CoursId = 18 },
            new EstDisponible { EnseignantId = 5, EmploiDuTempsEnseignantId = 22, CoursId = 19 },
            new EstDisponible { EnseignantId = 5, EmploiDuTempsEnseignantId = 12, CoursId = 20 },

            new EstDisponible { EnseignantId = 6, EmploiDuTempsEnseignantId = 21, CoursId = 29 },
            new EstDisponible { EnseignantId = 6, EmploiDuTempsEnseignantId = 24, CoursId = 30 },

            new EstDisponible { EnseignantId = 7, EmploiDuTempsEnseignantId = 24, CoursId = 21 },
            new EstDisponible { EnseignantId = 7, EmploiDuTempsEnseignantId = 24, CoursId = 22 },
            new EstDisponible { EnseignantId = 7, EmploiDuTempsEnseignantId = 24, CoursId = 23 },
            new EstDisponible { EnseignantId = 7, EmploiDuTempsEnseignantId = 24, CoursId = 24 },
            new EstDisponible { EnseignantId = 7, EmploiDuTempsEnseignantId = 24, CoursId = 25 }

            );
            this.Notifications.AddRange(
               new Notification { Id = 1, Lu = true, TypeNotification = "L'élève Proust Marcel s'est inscrite sur la plateforme le 14/12/2022 14:31" },
               new Notification { Id = 2, Lu = false, TypeNotification = "L'enseignant Dupond Jean s'est inscrit sur la plateforme le 13/12/2022 18:42" },
               new Notification { Id = 3, Lu = false, TypeNotification = "L'élève Duras Marguerite s'est inscrit sur la plateforme le 19/12/2022 17:04" },
               new Notification { Id = 4, Lu = false, TypeNotification = "L'enseignant Gave Charles s'est inscrit sur la plateforme le 15/12/2022 18:07" }

               );

            this.SaveChanges();
        }
    }
}