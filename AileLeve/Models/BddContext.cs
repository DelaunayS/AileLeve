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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etudie>().HasKey(e => new { e.CoursId, e.EleveId});
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
                new Matiere { Id = 1, Nom = "Mathématiques"},
                new Matiere { Id = 2, Nom = "Français"},
                new Matiere { Id = 3, Nom = "Anglais"},
                new Matiere { Id = 4, Nom = "Espagnol"},
                new Matiere { Id = 5, Nom = "Histoire"},
                new Matiere { Id = 6, Nom = "Géographie"},
                new Matiere { Id = 7, Nom = "EMC"},
                new Matiere { Id = 8, Nom = "Physique"},
                new Matiere { Id = 9, Nom = "Chimie"},
                new Matiere { Id = 10, Nom = "SVT"},
                new Matiere { Id = 11, Nom = "Philosophie"},
                new Matiere { Id = 12, Nom = "Technologie/Informatique"},
                new Matiere { Id = 13, Nom = "Allemand"},
                new Matiere { Id = 14, Nom = "Méditation"},
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
                new Utilisateur { Id = 1, Nom = "Dupond", Prenom = "Jean", AdresseId=1 },
                new Utilisateur { Id = 2, Nom = "Dupont", Prenom = "Yann", AdresseId = 2 },
                new Utilisateur { Id = 3, Nom = "Dylan", Prenom = "Bob", AdresseId = 3 },
                new Utilisateur { Id = 4, Nom = "Bibb", Prenom = "Justine" }
            );
            this.Profils.AddRange(
                new Profil { Id = 1, Telephone = "0625522552", Image = "/img/profil.jpg", Email = "dupondjean@gmail.fr" },
                new Profil { Id = 2, Telephone = "0725752552", Image = "/img/profil.jpg", Email = "dupontyann@yahoo.fr" },
                new Profil { Id = 3, Telephone = "0635528550", Image = "/img/profil.jpg", Email = "dylanbob@gmail.fr" },
                new Profil { Id = 4, Telephone = "0600548552", Image = "/img/profil.jpg", Email = "bibbjustine@hotmail.fr" }
            );
            this.Comptes.AddRange(
                new Compte { Id = 1, Identifiant = "DupondJ", Password = Dal.EncodeMD5("ddddd"), UtilisateurId = 1, ProfilId = 1 },
                new Compte { Id = 2, Identifiant = "DupontY", Password = Dal.EncodeMD5("dydyd"), UtilisateurId = 2, ProfilId = 2 },
                new Compte { Id = 3, Identifiant = "Bobby", Password = Dal.EncodeMD5("bbbbb"), UtilisateurId = 3, ProfilId = 3 },
                new Compte { Id = 4, Identifiant = "Juju", Password = Dal.EncodeMD5("jjjjj"), UtilisateurId = 4, ProfilId = 4 }
            );

            this.Adresses.AddRange(
                new Adresse { Id = 1, Rue = "avenue des Roses", Numero = 50, CodePostal = 38000, Ville = "Grenoble" },
                new Adresse { Id = 2, Rue = "chemin des marais", Numero = 2, CodePostal = 51290, Ville = "Saint-Remy-en-Bouzemont-Saint-Genest-et-Isson" },
                new Adresse { Id = 3, Rue = "impasse de l'espoir", Numero = 150, CodePostal = 67000, Ville = "Strasbourg"}
                );

            this.Enseignants.AddRange(
                new Enseignant { Id = 1, UtilisateurId = 2 },
                new Enseignant { Id = 2, UtilisateurId = 4 });

            this.Eleves.AddRange(
                new Eleve { Id = 1, DateDeNaissance = new DateTime(2006, 08, 15), UtilisateurId = 1},
                new Eleve { Id = 2, DateDeNaissance = new DateTime(2013, 12, 18), UtilisateurId = 3}
                );

            this.Cours.AddRange(
                
                new Cours { Id = 1, MatiereId = 1, NiveauId = 3, EnseignantId= 1, TypeCours = TypeCours.domicile},
                new Cours { Id = 2, MatiereId = 2, NiveauId = 1, EnseignantId = 1, TypeCours = TypeCours.onlineSynchrone}
            );

            this.Etudie.AddRange(
                new Etudie { EleveId = 2, CoursId = 1},
                new Etudie { EleveId = 2, CoursId = 2}
                );

            this.SaveChanges();
        }
    }
}