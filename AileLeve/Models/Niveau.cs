using System.ComponentModel.DataAnnotations;

namespace AileLeve.Models
{
    public class Niveau
    {
        public int Id { get; set; }
        [MaxLength(13)]
        public string Nom { get; set; }

    }
}
