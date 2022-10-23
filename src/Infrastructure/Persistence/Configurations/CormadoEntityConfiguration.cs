using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class CormadoEntityConfiguration : IEntityTypeConfiguration<CormadoEntity>
{
    public void Configure(EntityTypeBuilder<CormadoEntity> builder)
    {

        builder.ToTable("Cormados");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd(); 


        builder.HasIndex(s => s.Email).IsUnique();

        builder.Property(s => s.LegalName).IsRequired();
        builder.HasIndex(s => s.LegalName).IsUnique();

        builder.Property(s => s.CommercialName).IsRequired();
        builder.HasIndex(s => s.CommercialName).IsUnique();

        builder.Property(s => s.PointOfContact).IsRequired();

        builder.Property(s => s.Address).IsRequired();


    }
}
