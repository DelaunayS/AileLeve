using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;

namespace AileLeve.ViewModels
{
    public class UtilisateurCompletViewModel
    {
        public Compte Compte { get; set; }
        public Profil Profil { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}