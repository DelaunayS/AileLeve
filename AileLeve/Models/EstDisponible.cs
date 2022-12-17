using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AileLeve.Models
{
    public class EstDisponible
    {
        public int? EnseignantId { get; set; }
        public virtual Enseignant Enseignant { get; set; }

        public int? EmploiDuTempsEnseignantId { get; set; }

        public virtual EmploiDuTempsEnseignant EmploiDuTempsEnseignant { get; set; }

        public int? CoursId { get; set; }

        public virtual Cours Cours { get; set; }

    }
}
