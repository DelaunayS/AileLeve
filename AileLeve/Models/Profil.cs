using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Models
{
    public class Profil
    {
        public int Id { get; set; }
        [MaxLength(15)]
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }
        public string Image { get; set; }
        [MaxLength(40)]
        [Required(ErrorMessage = "Le champ Email doit être rempli.")]
        public string Email { get; set; }
    }
}