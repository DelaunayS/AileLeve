namespace AileLeve.Models
{
  
    public class Etudie
    {
        public int? EleveId { get; set; }
        public virtual Eleve Eleve { get; set; }
        public int CoursId { get; set; }
        public virtual Cours Cours { get; set; }
  
    }
}
