using System.ComponentModel.DataAnnotations;

namespace AileLeve.Models
{
    public class Niveau
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Nom { get; set; }

    }
}
