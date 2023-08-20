﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyHealth.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyHealth.Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230820065345_upd_FileStorages")]
    partial class upd_FileStorages
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyHealth.Data.Entities.FileStorage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("ID");

                    b.ToTable("FileStorages");
                });

            modelBuilder.Entity("MyHealth.Data.Entities.Metric", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<double?>("AbdominalGirth")
                        .HasColumnType("double precision");

                    b.Property<int?>("ArterialPressureLower")
                        .HasColumnType("integer");

                    b.Property<int?>("ArterialPressureUpper")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateFilling")
                        .HasColumnType("date");

                    b.Property<double?>("Height")
                        .HasColumnType("double precision");

                    b.Property<int?>("IntraocularPressure")
                        .HasColumnType("integer");

                    b.Property<int?>("Pulse")
                        .HasColumnType("integer");

                    b.Property<int?>("Saturation")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<double?>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("MyHealth.Data.Entities.PhoneVerificationData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("OTP")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<bool>("Сonfirmed")
                        .HasColumnType("boolean");

                    b.HasKey("ID");

                    b.ToTable("PhoneVerificationData");
                });

            modelBuilder.Entity("MyHealth.Data.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int?>("AvatarFileID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<int?>("Blood")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("FirebaseUid")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("INN")
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Phone")
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<int?>("RhFactor")
                        .HasColumnType("integer");

                    b.Property<int?>("Role")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("ID");

                    b.HasIndex("AvatarFileID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyHealth.Data.Entities.Metric", b =>
                {
                    b.HasOne("MyHealth.Data.Entities.User", "User")
                        .WithMany("Metrics")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyHealth.Data.Entities.User", b =>
                {
                    b.HasOne("MyHealth.Data.Entities.FileStorage", "AvatarFile")
                        .WithMany()
                        .HasForeignKey("AvatarFileID");

                    b.Navigation("AvatarFile");
                });

            modelBuilder.Entity("MyHealth.Data.Entities.User", b =>
                {
                    b.Navigation("Metrics");
                });
#pragma warning restore 612, 618
        }
    }
}
