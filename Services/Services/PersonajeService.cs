using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Services;
using Core.Responses;
using Services.Validators;

namespace Services.Services
{
    public class PersonajeService : IPersonajeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonajeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        

        public async Task<Personaje> Create(Personaje newPersonaje)
        {
            PersonajeValidators validator = new();


            var validationResult = await validator.ValidateAsync(newPersonaje);
            if (validationResult.IsValid)
            {
                //newPersonaje.tipo = await _unitOfWork.TipoPersonajeRepository.GetByIdAsync(newPersonaje.tipoId);

                //if(newPersonaje.tipo != null ) {

                await _unitOfWork.PersonajeRepository.AddAsync(newPersonaje);
                await _unitOfWork.CommitAsync();
                //}
                //else {
                //    throw new ArgumentException("El tipo de personaje no existe");
                //}
            }
            else
            {
                
                throw new ArgumentException(validationResult.Errors[0].ErrorMessage.ToString());
            }

            return newPersonaje;
        }

        public async Task Delete(int PersonajeId)
        {
            Personaje Personaje = await _unitOfWork.PersonajeRepository.GetByIdAsync(PersonajeId);
            if(Personaje != null)
            {
                _unitOfWork.PersonajeRepository.Remove(Personaje);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new Exception("El personaje no existe");
            }
        }

        public async Task<IEnumerable<Personaje>> GetAll()
        {
            return await _unitOfWork.PersonajeRepository.GetAllAsync();
        }

        public async Task<Personaje> GetById(int id)
        {
            return await _unitOfWork.PersonajeRepository.GetByIdAsync(id);
        }

        //private Personaje ValidarPersonaje(int id)
        //{
        //    Personaje PersonajeToBeUpdated = await _unitOfWork.PersonajeRepository.GetByIdAsync(PersonajeToBeUpdatedId);

        //    if (PersonajeToBeUpdated == null)
        //        throw new ArgumentException("Invalid Personaje ID while updating");
        //    else
        //        return PersonajeToBeUpdated;
        //}

        public async Task<Personaje> Update(int PersonajeToBeUpdatedId, Personaje newPersonajeValues)
        {
            PersonajeActualizarValidators PersonajeValidator = new();
            
            var validationResult = await PersonajeValidator.ValidateAsync(newPersonajeValues);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.ToString());

            Personaje PersonajeToBeUpdated = await _unitOfWork.PersonajeRepository.GetByIdAsync(PersonajeToBeUpdatedId);

            if (PersonajeToBeUpdated == null)
                throw new ArgumentException("Invalid Personaje ID while updating");

            //PersonajeToBeUpdated.tipoId = newPersonajeValues.tipoId;
            PersonajeToBeUpdated.nombre = newPersonajeValues.nombre;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.PersonajeRepository.GetByIdAsync(PersonajeToBeUpdatedId);
        }

        public async Task<Personaje> LevelUp(int PersonajeToBeUpdatedId)
        {
            PersonajeValidators PersonajeValidator = new();
            Personaje PersonajeToBeUpdated = await _unitOfWork.PersonajeRepository.GetByIdAsync(PersonajeToBeUpdatedId);
            if (PersonajeToBeUpdated == null)
                throw new ArgumentException("Invalid Personaje ID while updating");

            PersonajeToBeUpdated.experiencia = 0;
            PersonajeToBeUpdated.nivel += 1;
            PersonajeToBeUpdated.salud = PersonajeToBeUpdated.nivel * (new Random().Next(1,5) + 50);
            PersonajeToBeUpdated.energia = PersonajeToBeUpdated.nivel * 50;
            PersonajeToBeUpdated.inteligencia += new Random().Next(1,5);
            PersonajeToBeUpdated.agilidad += new Random().Next(1,5);
            PersonajeToBeUpdated.salud += new Random().Next(1,5);
            PersonajeToBeUpdated.inteligencia += new Random().Next(1,5);
            PersonajeToBeUpdated.fuerza += new Random().Next(1,5);
            
            var validationResult = await PersonajeValidator.ValidateAsync(PersonajeToBeUpdated);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.ToString());

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.PersonajeRepository.GetByIdAsync(PersonajeToBeUpdatedId);
        }

        public async Task<AtaqueResponse> Atacar(int idEnemigo, int idPersonaje){

            EnemigoService _servicioEnemigo = new EnemigoService(_unitOfWork);

            AtaqueResponse response = new AtaqueResponse();
            // buscar info enemico 
            var enemigo = await _unitOfWork.EnemigoRepository.GetByIdAsync(idEnemigo);
            var personaje = await _unitOfWork.PersonajeRepository.GetByIdAsync(idPersonaje);

            if(Math.Abs(enemigo.nivelAmenaza - personaje.nivel) <= 5){
                int puntosDañoEnemigo = 0;
                int puntosDaño = (int)(
                                    (20 + personaje.fuerza) * 1.5)
                                    - (int)(10 + enemigo.defensa * 1.75);
                enemigo.salud -=  puntosDaño;

                if(enemigo.salud > 0) {
                    puntosDañoEnemigo = (int)(
                                     (20 + enemigo.fuerza) * 1.5)
                                      - (int)(10 + personaje.defensa * 1.75);
                    personaje.salud -= puntosDañoEnemigo;

                }else{
                    personaje.experiencia += (enemigo.nivelAmenaza * 2);

                    if(personaje.nivel * 10 < personaje.experiencia){
                        await LevelUp(personaje.id);
                    }
                }
                response.personaje = personaje;
                response.enemigo = enemigo;
                response.mensaje = $"{personaje.nombre} atacó e inflingio {puntosDaño} a {enemigo.nombre} y sufrio un contraataque de {puntosDañoEnemigo}";
                await _unitOfWork.CommitAsync();
            }
            else{
                response.mensaje = "No es posible atacar al enemigo ";
            }


            return response;
        }

        public async Task<Personaje> AprenderHabilidad(int personajeToBeUpdatedId, int idHabilidad)
        {
            Personaje PersonajeToBeUpdated = await _unitOfWork.PersonajeRepository.GetByIdAsync(personajeToBeUpdatedId);
            Habilidad habilidad = await _unitOfWork.HabilidadRepository.GetByIdAsync(idHabilidad);
            if (PersonajeToBeUpdated == null)
                throw new ArgumentException("Invalid Personaje ID while updating");

            if (habilidad == null)
                throw new ArgumentException("ID de Habilidad Invalido");


            if (PersonajeToBeUpdated.habilidades.Where(Hab => Hab.id == idHabilidad).ToList().Count > 0)
                throw new ArgumentException("No se puede aprender la misma habilidad dos veces");


            PersonajeToBeUpdated.habilidades.Add(habilidad); //

            await _unitOfWork.CommitAsync();
            return PersonajeToBeUpdated;

        }

        public async Task<string> Moverse(int idPersonaje, int idUbicacion)
        {
            Personaje personaje = await _unitOfWork.PersonajeRepository.GetByIdAsync(idPersonaje);
            Ubicacion ubicacion = await _unitOfWork.UbicacionRepository.GetByIdAsync(idUbicacion);
            var enemigos = await _unitOfWork.EnemigoRepository.GetAllAsync();
            MoverseResponse response = new();

            if (personaje == null)
                throw new ArgumentException("Invalid Personaje ID while updating");

            if (ubicacion == null)
                throw new ArgumentException("Invalid Personaje ID while updating");

            //enemigos = enemigos.Where(x => personaje.nivel < x.nivelAmenaza < personaje.nivel+5);
            response.mensaje = $"El Personaje {personaje.nombre} se ha movido a {ubicacion.nombre}, el clima es {ubicacion.clima} y se ha encontrado con un enemigo nivel xx";
            return $"{personaje.nombre} a llegado a {ubicacion.nombre}, conocido por su clima {ubicacion.clima} y es emboscado por un enemigo nivel xx";
        }
    }
}
