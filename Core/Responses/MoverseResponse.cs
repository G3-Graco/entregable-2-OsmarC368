using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Responses
{
    public class MoverseResponse
    {
        public Enemigo enemigoEncontrado {get; set;}
        public string mensaje { get; set; }
    }
}