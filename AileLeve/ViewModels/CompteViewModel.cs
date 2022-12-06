using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AileLeve.Models;

namespace AileLeve.ViewModels
{
    public class CompteViewModel
    {
        public Compte Compte { get; set; }
        public bool Authentifie { get; set; }
    }
}