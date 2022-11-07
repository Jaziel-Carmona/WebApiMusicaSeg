﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiMusicaSeg;

#nullable disable

namespace WebApiMusicaSeg.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApiMusicaSeg.Entidades.Albums", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CancionId")
                        .HasColumnType("int");

                    b.Property<string>("Tema")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CancionId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("WebApiMusicaSeg.Entidades.Artista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Artistas");
                });

            modelBuilder.Entity("WebApiMusicaSeg.Entidades.ArtistaCancion", b =>
                {
                    b.Property<int>("ArtistaId")
                        .HasColumnType("int");

                    b.Property<int>("CancionId")
                        .HasColumnType("int");

                    b.Property<int>("Orden_Lanzamiento")
                        .HasColumnType("int");

                    b.HasKey("ArtistaId", "CancionId");

                    b.HasIndex("CancionId");

                    b.ToTable("ArtistaCancion");
                });

            modelBuilder.Entity("WebApiMusicaSeg.Entidades.Cancion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Canciones");
                });

            modelBuilder.Entity("WebApiMusicaSeg.Entidades.Albums", b =>
                {
                    b.HasOne("WebApiMusicaSeg.Entidades.Cancion", "Cancion")
                        .WithMany("Albums")
                        .HasForeignKey("CancionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cancion");
                });

            modelBuilder.Entity("WebApiMusicaSeg.Entidades.ArtistaCancion", b =>
                {
                    b.HasOne("WebApiMusicaSeg.Entidades.Artista", "Artista")
                        .WithMany("ArtistaCancion")
                        .HasForeignKey("ArtistaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiMusicaSeg.Entidades.Cancion", "Cancion")
                        .WithMany("ArtistaCancion")
                        .HasForeignKey("CancionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artista");

                    b.Navigation("Cancion");
                });

            modelBuilder.Entity("WebApiMusicaSeg.Entidades.Artista", b =>
                {
                    b.Navigation("ArtistaCancion");
                });

            modelBuilder.Entity("WebApiMusicaSeg.Entidades.Cancion", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("ArtistaCancion");
                });
#pragma warning restore 612, 618
        }
    }
}
