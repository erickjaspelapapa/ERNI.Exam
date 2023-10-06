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
    public class LedgerSchema : IEntityTypeConfiguration<LedgerModel>
    {
        public void Configure(EntityTypeBuilder<LedgerModel> builder)
        {
            builder.ToTable("ledger");

            builder.Property(x => x.trans_type)
                .HasColumnType("NVARCHAR(10)")
                .IsRequired();

            builder.Property(x => x.description)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(x => x.trans_amount)
                .HasColumnType("DECIMAL(10,2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasOne(f => f.ClientBase)
                .WithOne()
                .HasForeignKey<LedgerModel>(f => f.client_id);
        }
    }
}
