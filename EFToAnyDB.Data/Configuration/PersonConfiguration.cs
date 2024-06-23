using EFToAnyDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFToAnyDB.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.HasKey(_ => _.PersonId);

            entity.Property(_ => _.PersonId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(_ => _.DisplayName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
