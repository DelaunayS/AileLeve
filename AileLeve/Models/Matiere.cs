using System.ComponentModel.DataAnnotations;

namespace AileLeve.Models
{
    public class Matiere
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nom { get; set; }

    }
}
