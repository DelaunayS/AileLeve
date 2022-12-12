using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Models
{
    public class Compte
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Identifiant { get; set; }
        public int? UtilisateurId { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
        public int? ProfilId { get; set; }
        public virtual Profil Profil { get; set; }
        public bool statusActif { get; set; }
    }
}