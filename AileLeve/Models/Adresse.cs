using System.ComponentModel.DataAnnotations;

namespace AileLeve.Models
{
    public class Adresse
    {
        public int Id { get; set; }

    
        
        public int NumeroRue { get; set; }

        [MaxLength(30)]
        public string Rue { get; set; }

   
        public int CodePostal { get; set; }

        [MaxLength(50)]
        public string Ville { get; set; }

       

    }
}
