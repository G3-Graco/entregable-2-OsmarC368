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

        public async Task<Mision> AceptarMision(int idMision, int idPersonaje)
        {
            Mision misionToBeUpdated = await _unitOfWork.MisionRepository.GetByIdAsync(idMision);
            Personaje personaje = await _unitOfWork.PersonajeRepository.GetByIdAsync(idPersonaje);

            if (misionToBeUpdated == null)
                throw new ArgumentException("ID de la Mision Invalido");

            if (personaje == null)
                throw new ArgumentException("ID de Personaje Invalido");

            misionToBeUpdated.estado = 'p';

            await _unitOfWork.CommitAsync();

            return misionToBeUpdated;
        }

        public async Task<float> Progreso(int idMision)
        {
            Mision mision = await _unitOfWork.MisionRepository.GetByIdAsync(idMision);
            
            if (mision == null)
                throw new ArgumentException("ID de la Mision Invalido");

            var progreso = mision.objetivos.Where(x => x.Split("-")[1] == "terminada").ToList().Count / mision.objetivos.Count * 100;

            return progreso;
        }

        public async Task<IEnumerable<string>> CompletarMision(int idMision, int idPersonaje)
        {
            Mision misionToBeUpdated = await _unitOfWork.MisionRepository.GetByIdAsync(idMision);
            Personaje personaje = await _unitOfWork.PersonajeRepository.GetByIdAsync(idPersonaje);

            if (misionToBeUpdated == null)
                throw new ArgumentException("ID de la Mision Invalido");

            if (personaje == null)
                throw new ArgumentException("ID de Personaje Invalido");

            misionToBeUpdated.estado = 'p';

            await _unitOfWork.CommitAsync();

            return misionToBeUpdated.recompensas;
        }

    }
}