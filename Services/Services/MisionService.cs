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
    public class MisionService: IMisionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MisionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Mision> Create(Mision newMision)
        {
            MisionValidators validator = new();

            var validateResult = await validator.ValidateAsync(newMision);

            if (validateResult.IsValid)
            {
                await _unitOfWork.MisionRepository.AddAsync(newMision);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException(validateResult.Errors[0].ErrorMessage.ToString());
            }
            
            return newMision;
        }

        public async Task Delete(int idMision)
        {
            Mision mision = await _unitOfWork.MisionRepository.GetByIdAsync(idMision);

            if (mision == null)
                throw new ArgumentException("ID Mision Invalido!");

            _unitOfWork.MisionRepository.Remove(mision);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Mision>> GetAll()
        {
            return await _unitOfWork.MisionRepository.GetAllAsync();
        }

        public async Task<Mision> GetById(int idMision)
        {
            return await _unitOfWork.MisionRepository.GetByIdAsync(idMision);
        }

        public async Task<Mision> Update(int idMision, Mision newMision)
        {
            MisionValidators validator = new();

            var validatorResult = await validator.ValidateAsync(newMision);

            if(!validatorResult.IsValid)
                throw new ArgumentException(validatorResult.Errors[0].ErrorMessage.ToString());

            Mision misionToUpdate = await _unitOfWork.MisionRepository.GetByIdAsync(idMision);

            if (misionToUpdate == null)
                throw new ArgumentException("ID Mision Invalido!");

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.MisionRepository.GetByIdAsync(idMision);
        }

    }
}