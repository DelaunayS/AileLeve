using AileLeve.Models;
using System.Collections.Generic;

namespace AileLeve.ViewModels
{
    public class CoursViewModel
    {
        public Compte Compte { get; set; }
        public Enseignant Enseignant { get; set; }
        public Cours Cours { get; set; }
      
        public bool Authentifie { get; set; }
        public List<Cours> CoursListe { get; set; }
    }
}
