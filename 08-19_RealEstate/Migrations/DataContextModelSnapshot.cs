﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _08_19_RealEstate.Data;

namespace _08_19_RealEstate.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_08_19_RealEstate.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FlatNr")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("HouseNr")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<decimal>("AreaInSqm")
                        .HasPrecision(10, 3)
                        .HasColumnType("decimal(10,3)");

                    b.Property<int>("BrokerId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<int>("TotalFloorsInBuilding")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("BrokerId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.Broker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Brokers");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.CompanyBrokerJunction", b =>
                {
                    b.Property<int>("BrokerId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.HasKey("BrokerId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompaniesBrokers");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.Apartment", b =>
                {
                    b.HasOne("_08_19_RealEstate.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_08_19_RealEstate.Models.Broker", "Broker")
                        .WithMany("Apartments")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("_08_19_RealEstate.Models.Company", "Company")
                        .WithMany("Apartments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Broker");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.Company", b =>
                {
                    b.HasOne("_08_19_RealEstate.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.CompanyBrokerJunction", b =>
                {
                    b.HasOne("_08_19_RealEstate.Models.Broker", "Broker")
                        .WithMany()
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("_08_19_RealEstate.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Broker");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.Broker", b =>
                {
                    b.Navigation("Apartments");
                });

            modelBuilder.Entity("_08_19_RealEstate.Models.Company", b =>
                {
                    b.Navigation("Apartments");
                });
#pragma warning restore 612, 618
        }
    }
}