using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Services.Validators
{
    public class EquipoValidators: AbstractValidator<Equipo>
    {
        public EquipoValidators()
        {
            RuleFor(x => x.casco)
            .MaximumLength(255);

            RuleFor(x => x.armadura)
            .MaximumLength(255);

            RuleFor(x => x.arma1)
            .MaximumLength(255);

            RuleFor(x => x.arma2)
            .MaximumLength(255);

            RuleFor(x => x.guanteletes)
            .MaximumLength(255);

            RuleFor(x => x.grebas)
            .MaximumLength(255);
        }
    }
}