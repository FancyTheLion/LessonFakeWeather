﻿// <auto-generated />
using System;
using FakeWeatherBackend.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FakeWeatherBackend.DAO.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20240307210128_AddedPhotoPreview")]
    partial class AddedPhotoPreview
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FakeWeatherBackend.DAO.Models.FileDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FakeWeatherBackend.DAO.Models.WeatherDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Cloudiness")
                        .HasColumnType("double precision");

                    b.Property<double>("Humidity")
                        .HasColumnType("double precision");

                    b.Property<Guid>("PhotoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PhotoPreviewId")
                        .HasColumnType("uuid");

                    b.Property<double>("Pressure")
                        .HasColumnType("double precision");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.HasIndex("PhotoPreviewId");

                    b.ToTable("Weathers");
                });

            modelBuilder.Entity("FakeWeatherBackend.DAO.Models.WeatherDbo", b =>
                {
                    b.HasOne("FakeWeatherBackend.DAO.Models.FileDbo", "Photo")
                        .WithMany("PhotosForWeathers")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FakeWeatherBackend.DAO.Models.FileDbo", "PhotoPreview")
                        .WithMany("PhotosPreviewsForWeathers")
                        .HasForeignKey("PhotoPreviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");

                    b.Navigation("PhotoPreview");
                });

            modelBuilder.Entity("FakeWeatherBackend.DAO.Models.FileDbo", b =>
                {
                    b.Navigation("PhotosForWeathers");

                    b.Navigation("PhotosPreviewsForWeathers");
                });
#pragma warning restore 612, 618
        }
    }
}
