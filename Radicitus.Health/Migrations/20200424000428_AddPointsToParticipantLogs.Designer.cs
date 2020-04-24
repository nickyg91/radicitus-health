﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Radicitus.Health.Data.Contexts;

namespace Radicitus.Health.Migrations
{
    [DbContext(typeof(RadicitusHealthContext))]
    [Migration("20200424000428_AddPointsToParticipantLogs")]
    partial class AddPointsToParticipantLogs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Radicitus.Health.Data.Entities.HealthInitiative", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("StartDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("TotalWeightLossGoal")
                        .HasColumnType("decimal(4,1)");

                    b.HasKey("Id");

                    b.ToTable("HealthInitiative");
                });

            modelBuilder.Entity("Radicitus.Health.Data.Entities.HealthParticipant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("HealthInitiativeId")
                        .HasColumnType("integer");

                    b.Property<decimal>("IndividualWeightLossGoal")
                        .HasColumnType("decimal(4,1)");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<decimal>("StartingWeight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("HealthInitiativeId");

                    b.ToTable("HealthParticipant");
                });

            modelBuilder.Entity("Radicitus.Health.Data.Entities.ParticipantLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("CurrentWeight")
                        .HasColumnType("decimal(4,1)");

                    b.Property<DateTime>("DateSubmitted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("HealthInitiativeId")
                        .HasColumnType("integer");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.Property<byte>("Points")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("HealthInitiativeId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("ParticipantLog");
                });

            modelBuilder.Entity("Radicitus.Health.Data.Entities.HealthParticipant", b =>
                {
                    b.HasOne("Radicitus.Health.Data.Entities.HealthInitiative", "HealthInitiative")
                        .WithMany("HealthParticipants")
                        .HasForeignKey("HealthInitiativeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Radicitus.Health.Data.Entities.ParticipantLog", b =>
                {
                    b.HasOne("Radicitus.Health.Data.Entities.HealthInitiative", "HealthInitiative")
                        .WithMany()
                        .HasForeignKey("HealthInitiativeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Radicitus.Health.Data.Entities.HealthParticipant", "HealthParticipant")
                        .WithMany("ParticipantLogs")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
