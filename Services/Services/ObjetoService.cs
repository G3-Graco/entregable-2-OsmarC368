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
    public class ObjetoService: IObjetoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObjetoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Objeto> Create(Objeto newObjeto)
        {
            ObjetoValidators validator = new();

            var validateResult = await validator.ValidateAsync(newObjeto);

            if (validateResult.IsValid)
            {
                await _unitOfWork.ObjetoRepository.AddAsync(newObjeto);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(validateResult.Errors[0].ErrorMessage.ToString());
            }
            
            return newObjeto;
        }

        public async Task Delete(int idObjeto)
        {
            Objeto objeto = await _unitOfWork.ObjetoRepository.GetByIdAsync(idObjeto);

            if (objeto == null)
                throw new ArgumentException("ID Objeto Invalido!");

            _unitOfWork.ObjetoRepository.Remove(objeto);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Objeto>> GetAll()
        {
            return await _unitOfWork.ObjetoRepository.GetAllAsync();
        }

        public async Task<Objeto> GetById(int idObjeto)
        {
            return await _unitOfWork.ObjetoRepository.GetByIdAsync(idObjeto);
        }

        public async Task<Objeto> Update(int idObjeto, Objeto newObjeto)
        {
            ObjetoValidators validator = new();

            var validatorResult = await validator.ValidateAsync(newObjeto);

            if(!validatorResult.IsValid)
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());

            Objeto objetoToUpdate = await _unitOfWork.ObjetoRepository.GetByIdAsync(idObjeto);

            if (objetoToUpdate == null)
                throw new ArgumentException("ID Objeto Invalido!");
            
            objetoToUpdate.nombre = newObjeto.nombre;
            objetoToUpdate.descripcion = newObjeto.descripcion;
            objetoToUpdate.tipo = newObjeto.descripcion;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.ObjetoRepository.GetByIdAsync(idObjeto);
        }
    }
}