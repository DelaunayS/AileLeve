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
        [Required(ErrorMessage = "Le champ mot de passe doit être rempli.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Le champ Identifiant doit être rempli.")]
        public string Identifiant { get; set; }
        public int? UtilisateurId { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
        public int? ProfilId { get; set; }
        public virtual Profil Profil { get; set; }
        public bool StatusActif { get; set; }
    }
}