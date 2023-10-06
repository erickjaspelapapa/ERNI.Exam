using ERNI.Data.Model;
using ERNI.Data.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Data.Data
{
    public class ERNIContext  : DbContext
    {
        public ERNIContext(DbContextOptions<ERNIContext> option): base(option) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientSchema).Assembly);
        }

        public DbSet<ClientModel> client{ get; set; }
        public DbSet<AccountModel> account{ get; set; }
        public DbSet<LedgerModel> ledger { get; set; }
    }
}
