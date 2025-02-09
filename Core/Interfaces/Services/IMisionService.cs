using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IMisionService: IBaseService<Mision>
    {
        Task<Mision> AceptarMision(int idMision, int idPersonaje);
        Task<float> Progreso(int idMision);
        Task<IEnumerable<string>> CompletarMision(int idMision, int idPersonaje);
    }
}