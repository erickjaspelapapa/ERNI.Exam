﻿// <auto-generated />
using System;
using ERNI.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ERNI.Data.Migrations
{
    [DbContext(typeof(ERNIContext))]
    [Migration("20231004140313_AccountLedgerSchema")]
    partial class AccountLedgerSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ERNI.Data.Model.AccountModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<decimal>("account_balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DECIMAL(10,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("client_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("client_id")
                        .IsUnique();

                    b.ToTable("account", (string)null);
                });

            modelBuilder.Entity("ERNI.Data.Model.ClientModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email_address")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<int?>("protected_pin")
                        .HasColumnType("INT");

                    b.HasKey("id");

                    b.ToTable("client", (string)null);
                });

            modelBuilder.Entity("ERNI.Data.Model.LedgerModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("client_id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<decimal>("trans_amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DECIMAL(10,2")
                        .HasDefaultValue(0m);

                    b.Property<string>("trans_type")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(10)");

                    b.HasKey("id");

                    b.HasIndex("client_id")
                        .IsUnique();

                    b.ToTable("ledger", (string)null);
                });

            modelBuilder.Entity("ERNI.Data.Model.AccountModel", b =>
                {
                    b.HasOne("ERNI.Data.Model.ClientModel", "ClientBase")
                        .WithOne()
                        .HasForeignKey("ERNI.Data.Model.AccountModel", "client_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientBase");
                });

            modelBuilder.Entity("ERNI.Data.Model.LedgerModel", b =>
                {
                    b.HasOne("ERNI.Data.Model.ClientModel", "ClientBase")
                        .WithOne()
                        .HasForeignKey("ERNI.Data.Model.LedgerModel", "client_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientBase");
                });
#pragma warning restore 612, 618
        }
    }
}
