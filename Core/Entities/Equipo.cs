using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Entities;

namespace Core.Entities
{
    public class Equipo : IEquipo
    {
        public int id { get; set; }
        public string casco { get; set; } = string.Empty;
        public string armadura { get; set; } = string.Empty;
        public string arma1 { get; set; } = string.Empty;
        public string arma2 { get; set; } = string.Empty;
        public string guanteletes { get; set; } = string.Empty;
        public string grebas { get; set; } = string.Empty;
        //public List<Personaje> personajes { get; set; } = [];
    }
}