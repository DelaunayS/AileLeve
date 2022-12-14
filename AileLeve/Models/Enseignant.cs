using System.ComponentModel.DataAnnotations;

namespace AileLeve.Models
{
    public class Enseignant
    {
        public int Id { get; set; }
        public int? UtilisateurId { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
        
        //public int CoursId { get; set; }

    }
}
