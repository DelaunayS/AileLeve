using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;

namespace AileLeve.ViewModels
{
    public class CompteViewModel
    {
        public Compte Compte { get; set; }
        public string MotDePasse { get; set; }
        public bool Authentifie { get; set; }

        public Utilisateur Utilisateur { get; set; }
        public Profil Profil { get; set; }
        public Enseignant Enseignant { get; set; }
        public Eleve Eleve { get; set; }

        public EstDisponible EstDisponible { get; set; }
        public Etudie Etudie { get; set; }

        public EmploiDuTempsEnseignant EmpEns { get; set; }

        public List<Cours> ListeSimpleCours { get; set; }

        public List<EstDisponible> ToutesLesPropositionsDeCours { get; set; }
        public (List<EstDisponible>, List<Etudie>) CoursListe { get; set; }
        public (List<Etudie>, List<EstDisponible>) CoursListeEleve { get; set; }
        public Cours Cours { get; set; }
        public RepresentantLegal RL { get; set; }


    }
}