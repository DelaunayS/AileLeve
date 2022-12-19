using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Models
{
    public class RepresentantLegal
    {
        public int Id { get; set; }
        public string NomRL { get; set; }
        public string PrenomRL { get; set; }  

        public  int? EleveId { get; set; }
        public virtual Eleve Eleve { get; set; }


    }
}
