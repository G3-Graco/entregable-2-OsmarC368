using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class UbicacionValidators: AbstractValidator<Ubicacion>
    {
        public UbicacionValidators()
        {
            RuleFor(x => x.nombre)
            .MaximumLength(255);

            RuleFor(x => x.descripcion)
            .MaximumLength(255);

            RuleFor(x => x.clima)
            .MaximumLength(255);
        }
    }
}