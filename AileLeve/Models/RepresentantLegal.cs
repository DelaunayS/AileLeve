using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Models
{
    public class RepresentantLegal
    {
        public int Id { get; set; }
       
        public int? UtilisateurId { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }

        public  int? EleveId { get; set; }
        public virtual Eleve Eleve { get; set; }


    }
}
