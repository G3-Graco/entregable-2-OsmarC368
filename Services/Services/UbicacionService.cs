using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Services.Validators;

namespace Services.Services
{
    public class UbicacionService: IUbicacionService
    {
        private IUnitOfWork _unitOfWork;

        public UbicacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Ubicacion> Create(Ubicacion newUbicacion)
        {
            UbicacionValidators validator = new();

            var validatorResult = await validator.ValidateAsync(newUbicacion);

            if (validatorResult.IsValid)
            {
                await _unitOfWork.UbicacionRepository.AddAsync(newUbicacion);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());
            }

            return newUbicacion;
        }

        public async Task Delete(int idUbicacion)
        {
            Ubicacion ubicacion = await _unitOfWork.UbicacionRepository.GetByIdAsync(idUbicacion);

            if (ubicacion == null)
                throw new ArgumentException("ID Ubicacion Invalido!!!!");

            _unitOfWork.UbicacionRepository.Remove(ubicacion);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Ubicacion>> GetAll()
        {
            return await _unitOfWork.UbicacionRepository.GetAllAsync();
        }

        public async Task<Ubicacion> GetById(int idUbicacion)
        {
            return await _unitOfWork.UbicacionRepository.GetByIdAsync(idUbicacion);
        }

        public async Task<Ubicacion> Update(int idUbicacion, Ubicacion newUbicacion)
        {
            UbicacionValidators validator = new();

            var validatorResult = await validator.ValidateAsync(newUbicacion);

            if(!validatorResult.IsValid)
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());

            Ubicacion ubicacionToUpdate = await _unitOfWork.UbicacionRepository.GetByIdAsync(idUbicacion);

            if (ubicacionToUpdate == null)
                throw new ArgumentException("ID Equipo Invalido!");

            ubicacionToUpdate.nombre = newUbicacion.nombre;
            ubicacionToUpdate.descripcion = newUbicacion.descripcion;
            await _unitOfWork.CommitAsync();

            return await _unitOfWork.UbicacionRepository.GetByIdAsync(idUbicacion);
        }
    }
}