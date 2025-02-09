using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class ObjetoValidators: AbstractValidator<Objeto>
    {
        public ObjetoValidators()
        {
            RuleFor(x => x.nombre)
            .MaximumLength(255);
        }
    }
}