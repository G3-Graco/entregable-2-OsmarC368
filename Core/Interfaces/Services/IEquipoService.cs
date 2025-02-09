using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IEquipoService: IBaseService<Equipo>
    {
        Task<Equipo> EquiparObjeto(int idObjeto, int idEquipo, string type);
        Task<Equipo> DesequiparObjeto(int idObjeto, int idEquipo);
        
    }
}