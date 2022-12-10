using System.ComponentModel.DataAnnotations;

namespace AileLeve.Models
{
    public class Adresse
    {
        public int Id { get; set; }

        [MaxLength(5)]
        public int Numero { get; set; }

        [MaxLength(30)]
        public string Rue { get; set; }

        [MaxLength(50)]
        public string Ville { get; set; }

        [MaxLength(5)]
        public int CodePostal { get; set; }

    }
}
