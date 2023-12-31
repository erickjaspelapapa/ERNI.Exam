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
    [Migration("20231004134239_ClientSchema")]
    partial class ClientSchema
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
#pragma warning restore 612, 618
        }
    }
}
