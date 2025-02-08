using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PersonajeConfiguration : IEntityTypeConfiguration<Personaje>
    {
        public void Configure(EntityTypeBuilder<Personaje> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.nombre).IsRequired().HasMaxLength(255);
            builder.Property(x => x.salud).IsRequired();
            builder.Property(x => x.energia).IsRequired();
            builder.Property(x => x.fuerza).IsRequired();
            builder.Property(x => x.inteligencia).IsRequired();
            builder.Property(x => x.agilidad).IsRequired();
            builder.Property(x => x.nivel).IsRequired();
            //builder.Property(x => x.idTipoPersonaje).IsRequired();
            builder.Property(x => x.equipo).IsRequired();
            builder.Property(x => x.experiencia).IsRequired();
            builder.HasMany(p => p.habilidades).WithMany(habilidad => habilidad.personajes);
            builder.HasOne(p => p.tipo).WithMany().HasForeignKey(p => p.tipoId);
            builder.ToTable("Personaje");
        }
    }
}