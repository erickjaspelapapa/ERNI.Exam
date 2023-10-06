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
    public class AccountSchema : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("account");

            builder.Property(x => x.account_balance)
                .HasColumnType("DECIMAL(10,2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasOne(x => x.ClientBase)
                 .WithOne()
                 .HasForeignKey<AccountModel>(f => f.client_id);
        }
    }
}
