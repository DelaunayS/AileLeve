using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace AileLeve.ViewModels
{
    public class CoursViewModel
    {
        public Compte Compte { get; set; }
        public Enseignant Enseignant { get; set; }
        public Cours Cours { get; set; }
      
        public EstDisponible EstDisponible { get; set; }    
        public bool Authentifie { get; set; }
        public List<Cours> CoursListe { get; set; }

        //public List<Cours> CoursListePourAdmin { get; set; }

        public Utilisateur Utilisateur { get; set; }
        public string Role { get; set; }
    }
}