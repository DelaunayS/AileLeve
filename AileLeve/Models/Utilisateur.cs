using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Nom { get; set; }
         [MaxLength(30)]
        public string Prenom { get; set; }
    }
}