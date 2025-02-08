using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EquipoConfiguration: IEntityTypeConfiguration<Equipo>
    {
        public void Configure(EntityTypeBuilder<Equipo> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.casco).IsRequired().HasMaxLength(255); 
            builder.Property(x => x.armadura).IsRequired().HasMaxLength(255); 
            builder.Property(x => x.arma1).IsRequired().HasMaxLength(255); 
            builder.Property(x => x.arma2).IsRequired().HasMaxLength(255); 
            builder.Property(x => x.guanteletes).IsRequired().HasMaxLength(255); 
            builder.Property(x => x.grebas).IsRequired().HasMaxLength(255);

            //builder.HasMany(h=> h.personajes).WithMany(per => per.equipo);

            builder.ToTable("Equipo");
        }
    }
}