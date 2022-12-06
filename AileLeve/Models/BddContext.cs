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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=#Badaboum44;database=AileLeve");
        }
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            this.Utilisateurs.AddRange(
                new Utilisateur { Id = 1, Nom = "Dupond", Prenom = "Jean" },
                new Utilisateur { Id = 2, Nom = "Dupont", Prenom = "Yann" },
                new Utilisateur { Id = 3, Nom = "Dylan", Prenom = "Bob" },
                new Utilisateur { Id = 4, Nom = "Bibb", Prenom = "Justine" }
            );

            this.Profils.AddRange(
                new Profil { Id = 1, Telephone="0625522552", Image="Profil1", Email="dupondjean@gmail.fr"},
                new Profil { Id = 2, Telephone="0725752552", Image="Profil2", Email="dupontyann@yahoo.fr"},
                new Profil { Id = 3, Telephone="0635528550", Image="Profil3", Email="dylanbob@gmail.fr"},
                new Profil { Id = 4, Telephone="0600548552", Image="Profil4", Email="bibbjustine@hotmail.fr"}                
            );
            this.Comptes.AddRange(
                new Compte { Id = 1, Identifiant="DupondJ", Password="ddddd", UtilisateurId=1, ProfilId=1},
                new Compte { Id = 2, Identifiant="DupontY", Password="dydyd", UtilisateurId=2, ProfilId=2},
                new Compte { Id = 3, Identifiant="Bobby", Password="bbbbb", UtilisateurId=3, ProfilId=3},
                new Compte { Id = 4, Identifiant="Juju", Password="jjjjj", UtilisateurId=4, ProfilId=4}
            );
            this.SaveChanges();    
        }
    }
}