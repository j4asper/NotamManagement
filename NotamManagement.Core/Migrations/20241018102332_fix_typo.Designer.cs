﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotamManagement.Core.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NotamManagement.Core.Migrations
{
    [DbContext(typeof(NotamManagementContext))]
    [Migration("20241018102332_fix_typo")]
    partial class fix_typo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AirportFlightPlan", b =>
                {
                    b.Property<int>("AirportsId")
                        .HasColumnType("integer");

                    b.Property<int>("FlightPlansId")
                        .HasColumnType("integer");

                    b.HasKey("AirportsId", "FlightPlansId");

                    b.HasIndex("FlightPlansId");

                    b.ToTable("AirportFlightPlan");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FIR")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ICAO")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.Coordinates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.Property<int>("LowerFlightLevel")
                        .HasColumnType("integer");

                    b.Property<int>("NotamId")
                        .HasColumnType("integer");

                    b.Property<int>("UpperFlightLevel")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NotamId")
                        .IsUnique();

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.FlightPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("FlightPlans");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.Notam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EffectiveValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FIR")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NotamOffice")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("QCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReferenceIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Notams");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.NotamAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("Importance")
                        .HasColumnType("integer");

                    b.Property<int>("NotamId")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NotamId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("NotamActions");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AirportFlightPlan", b =>
                {
                    b.HasOne("NotamManagement.Core.Models.Airport", null)
                        .WithMany()
                        .HasForeignKey("AirportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotamManagement.Core.Models.FlightPlan", null)
                        .WithMany()
                        .HasForeignKey("FlightPlansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotamManagement.Core.Models.Coordinates", b =>
                {
                    b.HasOne("NotamManagement.Core.Models.Notam", "Notam")
                        .WithOne("Coordinates")
                        .HasForeignKey("NotamManagement.Core.Models.Coordinates", "NotamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notam");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.FlightPlan", b =>
                {
                    b.HasOne("NotamManagement.Core.Models.Organization", null)
                        .WithMany("FlightPlans")
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.NotamAction", b =>
                {
                    b.HasOne("NotamManagement.Core.Models.Notam", "Notam")
                        .WithMany()
                        .HasForeignKey("NotamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotamManagement.Core.Models.Organization", null)
                        .WithMany("NotamActions")
                        .HasForeignKey("OrganizationId");

                    b.Navigation("Notam");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.User", b =>
                {
                    b.HasOne("NotamManagement.Core.Models.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("NotamManagement.Core.Models.Notam", b =>
                {
                    b.Navigation("Coordinates")
                        .IsRequired();
                });

            modelBuilder.Entity("NotamManagement.Core.Models.Organization", b =>
                {
                    b.Navigation("FlightPlans");

                    b.Navigation("NotamActions");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
