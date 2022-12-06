using AileLeve.Models;
using System;
using System.Collections.Generic;
using Xunit;


namespace AileleveTest
{
    public class UnitTest1
    {
        [Fact]
        public void Creation_Utilisateur_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.CreerUtilisateur("Rouget", "Lulu");

                List<Utilisateur> utilisateurs = dal.ObtenirTousLesUtilisateurs();
                Assert.NotNull(utilisateurs);
                Assert.Single(utilisateurs);
                Assert.Equal("Rouget", utilisateurs[0].Nom);
                Assert.Equal("Lulu", utilisateurs[0].Prenom);
            }
        }
        [Fact]
        public void Creation_Profil_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.CreerProfil("06 06 06 06 06","SrcImage", "Lulu@gmail.com");

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
                dal.CreerUtilisateur("Rouget", "Lulu");
                dal.CreerProfil("06 06 06 06 06","SrcImage", "Lulu@gmail.com");
                dal.CreerCompte("Rouget", "rrrr",1,1);

                List<Compte> comptes = dal.ObtenirTousLesComptes();
                Assert.NotNull(comptes);
                Assert.Single(comptes);
                Assert.Equal("Rouget", comptes[0].Identifiant);
                Assert.Equal("rrrr", comptes[0].Password);
               
            }
        }
    }
}
