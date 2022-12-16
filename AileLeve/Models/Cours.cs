namespace AileLeve.Models
{
    public enum TypeCours
    {
        domicile, synchrone, asynchrone
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
