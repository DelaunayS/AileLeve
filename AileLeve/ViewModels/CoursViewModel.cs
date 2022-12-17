using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;


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
    }
}