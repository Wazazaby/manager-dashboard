﻿// <auto-generated />
using System;
using DashboardManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DashboardManager.Migrations
{
    [DbContext(typeof(DashboardManagerContext))]
    partial class DashboardManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DashboardManager.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CommercialId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartementId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommercialId");

                    b.HasIndex("DepartementId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("DashboardManager.Models.Commercial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CatchmentArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartementId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbContracts")
                        .HasColumnType("int");

                    b.Property<int>("NbQuotes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartementId");

                    b.ToTable("Commercial");
                });

            modelBuilder.Entity("DashboardManager.Models.Departement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departement");
                });

            modelBuilder.Entity("DashboardManager.Models.Client", b =>
                {
                    b.HasOne("DashboardManager.Models.Commercial", null)
                        .WithMany("Clients")
                        .HasForeignKey("CommercialId");

                    b.HasOne("DashboardManager.Models.Departement", "Departement")
                        .WithMany()
                        .HasForeignKey("DepartementId");
                });

            modelBuilder.Entity("DashboardManager.Models.Commercial", b =>
                {
                    b.HasOne("DashboardManager.Models.Departement", "Departement")
                        .WithMany()
                        .HasForeignKey("DepartementId");
                });
#pragma warning restore 612, 618
        }
    }
}
