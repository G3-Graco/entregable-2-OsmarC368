using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using FluentValidation.Validators;
using Services.Validators;

namespace Services.Services
{
    public class EquipoService: IEquipoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Equipo> Create(Equipo newEquipo)
        {
            EquipoValidators validator = new();

            var validateResult = await validator.ValidateAsync(newEquipo);

            if (validateResult.IsValid)
            {
                await _unitOfWork.EquipoRepository.AddAsync(newEquipo);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(validateResult.Errors[0].ErrorMessage.ToString());
            }
            
            return newEquipo;
        }

        public async Task Delete(int idEquipo)
        {
            Equipo equipo = await _unitOfWork.EquipoRepository.GetByIdAsync(idEquipo);

            if (equipo == null)
                throw new ArgumentException("ID Equipo Invalido!");

            _unitOfWork.EquipoRepository.Remove(equipo);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Equipo>> GetAll()
        {
            return await _unitOfWork.EquipoRepository.GetAllAsync();
        }

        public async Task<Equipo> GetById(int idEquipo)
        {
            return await _unitOfWork.EquipoRepository.GetByIdAsync(idEquipo);
        }

        public async Task<Equipo> Update(int idEquipo, Equipo newEquipo)
        {
            EquipoValidators validator = new();

            var validatorResult = await validator.ValidateAsync(newEquipo);

            if(!validatorResult.IsValid)
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());

            Equipo equipoToUpdate = await _unitOfWork.EquipoRepository.GetByIdAsync(idEquipo);

            if (equipoToUpdate == null)
                throw new ArgumentException("ID Equipo Invalido!");

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.EquipoRepository.GetByIdAsync(idEquipo);
        }

        public async Task<Equipo> EquiparObjeto(int idObjeto, int idEquipo, string type)
        {
            Equipo equipoToBeUpdated = await _unitOfWork.EquipoRepository.GetByIdAsync(idEquipo);
            Objeto objeto = await _unitOfWork.ObjetoRepository.GetByIdAsync(idObjeto);

            if (equipoToBeUpdated == null)
                throw new ArgumentException("Invalid Equipo ID while updating");

            if (objeto == null)
                throw new ArgumentException("ID de Objeto Invalido");

            switch(type)
            {
                case "casco":
                    if (objeto.tipo != "casco")
                        throw new ArgumentException("Tipo De Objeto no Coincide con el lugar al que se quiere equipar");
                    equipoToBeUpdated.casco = idObjeto.ToString();
                    break;
                case "armadura":
                    if (objeto.tipo != "armadura")
                        throw new ArgumentException("Tipo De Objeto no Coincide con el lugar al que se quiere equipar");
                    equipoToBeUpdated.armadura = idObjeto.ToString();
                    break;
                case "arma1":
                    if (objeto.tipo != "arma1")
                        throw new ArgumentException("Tipo De Objeto no Coincide con el lugar al que se quiere equipar");
                    equipoToBeUpdated.arma1 = idObjeto.ToString();
                    break;
                case "arma2":
                    if (objeto.tipo != "arma2")
                        throw new ArgumentException("Tipo De Objeto no Coincide con el lugar al que se quiere equipar");
                    equipoToBeUpdated.arma2 = idObjeto.ToString();
                    break;
                case "guanteletes":
                    if (objeto.tipo != "guanteletes")
                        throw new ArgumentException("Tipo De Objeto no Coincide con el lugar al que se quiere equipar");
                    equipoToBeUpdated.guanteletes = idObjeto.ToString();
                    break;
                case "grebas":
                    if (objeto.tipo != "grebas")
                        throw new ArgumentException("Tipo De Objeto no Coincide con el lugar al que se quiere equipar");
                    equipoToBeUpdated.grebas = idObjeto.ToString();
                    break;
                default:
                    throw new ArgumentException("Tipo de Objeto Invalido");
            }

            // switch(true)
            // {
            //     case true when objeto.tipo == "casco":
            //         // if (objeto.tipo != "casco")
            //         //     throw new ArgumentException("Tipo De Objeto no Coincide con el lugar al que se quiere equipar");
            //         equipoToBeUpdated.casco = idObjeto.ToString();
            //         break;
            //     case true when objeto.tipo == "armadura":
            //         equipoToBeUpdated.armadura = idObjeto.ToString();
            //         break;
            //     case true when objeto.tipo == "arma1":
            //         equipoToBeUpdated.arma1 = idObjeto.ToString();
            //         break;
            //     case true when objeto.tipo == "arma2":
            //         equipoToBeUpdated.arma2 = idObjeto.ToString();
            //         break;
            //     case true when objeto.tipo == "guanteletes":
            //         equipoToBeUpdated.guanteletes = idObjeto.ToString();
            //         break;
            //     case true when objeto.tipo == "grebas":
            //         equipoToBeUpdated.grebas = idObjeto.ToString();
            //         break;
            //     default:
            //         throw new ArgumentException("Tipo de Objeto Invalido");
            // }

            await _unitOfWork.CommitAsync();

            return equipoToBeUpdated;
        }

        public async Task<Equipo> DesequiparObjeto(int idObjeto, int idEquipo)
        {
            Equipo equipoToBeUpdated = await _unitOfWork.EquipoRepository.GetByIdAsync(idEquipo);

            if (equipoToBeUpdated == null)
                throw new ArgumentException("Invalid Equipo ID while updating");

            switch(true)
            {
                case true when equipoToBeUpdated.casco == idObjeto.ToString():
                    equipoToBeUpdated.casco = "Empty";
                    break;
                case true when equipoToBeUpdated.armadura == idObjeto.ToString():
                    equipoToBeUpdated.armadura = "Empty";
                    break;
                case true when equipoToBeUpdated.arma1 == idObjeto.ToString():
                    equipoToBeUpdated.arma1 = "Empty";
                    break;
                case true when equipoToBeUpdated.arma2 == idObjeto.ToString():
                    equipoToBeUpdated.arma2 = "Empty";
                    break;
                case true when equipoToBeUpdated.guanteletes == idObjeto.ToString():
                    equipoToBeUpdated.guanteletes = "Empty";
                    break;
                case true when equipoToBeUpdated.grebas == idObjeto.ToString():
                    equipoToBeUpdated.grebas = "Empty";
                    break;
                default:
                    throw new ArgumentException("ID Objeto Invalido");
            }

            await _unitOfWork.CommitAsync();
            return equipoToBeUpdated;
        }
    }
}