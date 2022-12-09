using AileLeve.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace AileLeve.Models
{
    public class Eleve
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateDeNaissance { get; set; }
        public int? UtilisateurId { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }

        
    }
}
