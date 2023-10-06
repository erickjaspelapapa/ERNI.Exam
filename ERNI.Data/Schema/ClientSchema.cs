using ERNI.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Data.Schema
{
    public class ClientSchema : IEntityTypeConfiguration<ClientModel>
    {
        public void Configure(EntityTypeBuilder<ClientModel> builder)
        {
            builder.ToTable("client");

            builder.Property(x => x.email_address)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();

            builder.Property(x => x.password)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();

            builder.Property(x => x.protected_pin)
                .HasColumnType("INT");
        }
    }
}
