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

        [Required]
        public int? UtilisateurId { get; set; }

        [Required]
        public virtual Utilisateur Utilisateur { get; set;}
        
        [Required]
        public int? ProfilId { get; set; }

        [Required]
        public virtual Profil Profil { get; set; }


    }
}