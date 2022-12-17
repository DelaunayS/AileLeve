using System.ComponentModel.DataAnnotations;

namespace AileLeve.Models
{
    public enum TypeCours
    {
        [Display(Name = "Cours à domicile")]
        domicile,
        [Display(Name = "Cours synchrone en ligne")]
        onlineSynchrone,
        [Display(Name = "Cours asynchrone")]
        onlineAsynchrone
    }
    
    public class Cours
    {
        public int Id { get; set; }
        public TypeCours TypeCours { get; set; }
        public int MatiereId { get; set; }
        public virtual Matiere Matiere { get; set; }    
        public int NiveauId { get; set; }  
        public virtual Niveau Niveau { get; set; }
        public int EnseignantId { get; set; }
        public virtual Enseignant Enseignant { get; set; }
    }
}
