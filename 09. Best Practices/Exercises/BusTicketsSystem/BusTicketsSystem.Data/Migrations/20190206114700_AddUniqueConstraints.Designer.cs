﻿// <auto-generated />
using System;
using BusTicketsSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusTicketsSystem.Data.Migrations
{
    [DbContext(typeof(BusTicketsContext))]
    [Migration("20190206114700_AddUniqueConstraints")]
    partial class AddUniqueConstraints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusTicketsSystem.Models.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<int>("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.Property<double>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0.0);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("BusCompanies");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("TownId");

                    b.HasKey("Id");

                    b.HasIndex("TownId");

                    b.ToTable("BusStations");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.Property<int>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(2);

                    b.Property<int>("HomeTownId");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("HomeTownId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusStationId");

                    b.Property<string>("Content")
                        .HasMaxLength(255);

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("DateTimeOfPublishing")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<double>("Grade")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0.0);

                    b.HasKey("Id");

                    b.HasIndex("BusStationId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0m);

                    b.Property<string>("Seat")
                        .IsRequired();

                    b.Property<int>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TripId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArrivalTime");

                    b.Property<int>("BusCompanyId");

                    b.Property<DateTime>("DepartureTime");

                    b.Property<int>("DestinationBusStationId");

                    b.Property<int>("OriginBusStationId");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("BusCompanyId");

                    b.HasIndex("DestinationBusStationId");

                    b.HasIndex("OriginBusStationId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BankAccount", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithMany("BankAccounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusCompany", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Country", "Country")
                        .WithMany("BusCompanies")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.BusStation", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Town", "Town")
                        .WithMany("BusStations")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Customer", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Town", "HomeTown")
                        .WithMany("Customers")
                        .HasForeignKey("HomeTownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Review", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.BusStation", "BusStation")
                        .WithMany("Reviews")
                        .HasForeignKey("BusStationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Ticket", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Customer", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BusTicketsSystem.Models.Trip", "Trip")
                        .WithMany("Tickets")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Town", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.Country", "Country")
                        .WithMany("Towns")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketsSystem.Models.Trip", b =>
                {
                    b.HasOne("BusTicketsSystem.Models.BusCompany", "BusCompany")
                        .WithMany("Trips")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketsSystem.Models.BusStation", "DestinationBusStation")
                        .WithMany("TripsToHere")
                        .HasForeignKey("DestinationBusStationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketsSystem.Models.BusStation", "OriginBusStation")
                        .WithMany("TripsStartsFromHere")
                        .HasForeignKey("OriginBusStationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
