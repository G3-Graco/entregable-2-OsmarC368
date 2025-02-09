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

        public async Task<Equipo> EquiparObjeto(int idObjeto, int idEquipo)
        {
            Equipo equipoToBeUpdated = await _unitOfWork.EquipoRepository.GetByIdAsync(idEquipo);
            Objeto objeto = await _unitOfWork.ObjetoRepository.GetByIdAsync(idObjeto);

            if (equipoToBeUpdated == null)
                throw new ArgumentException("Invalid Equipo ID while updating");

            if (objeto == null)
                throw new ArgumentException("ID de Objeto Invalido");

            return equipoToBeUpdated;
        }

        public async Task<Equipo> DesequiparObjeto(int idObjeto, int idEquipo)
        {
            Equipo equipoToBeUpdated = await _unitOfWork.EquipoRepository.GetByIdAsync(idEquipo);
            Objeto objeto = await _unitOfWork.ObjetoRepository.GetByIdAsync(idObjeto);

            if (equipoToBeUpdated == null)
                throw new ArgumentException("Invalid Equipo ID while updating");

            if (objeto == null)
                throw new ArgumentException("ID de Objeto Invalido");

            return equipoToBeUpdated;
        }
    }
}