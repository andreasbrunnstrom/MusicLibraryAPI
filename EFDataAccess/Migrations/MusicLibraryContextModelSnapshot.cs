﻿// <auto-generated />
using System;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFDataAccess.Migrations
{
    [DbContext(typeof(MusicLibraryContext))]
    partial class MusicLibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("EFDataAccess.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<int>("NumberOfSongs")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("EFDataAccess.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<int>("NumberOfSongs")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("EFDataAccess.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int?>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("EFDataAccess.Models.Album", b =>
                {
                    b.HasOne("EFDataAccess.Models.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("EFDataAccess.Models.Song", b =>
                {
                    b.HasOne("EFDataAccess.Models.Album", "Album")
                        .WithMany("songs")
                        .HasForeignKey("AlbumId");

                    b.HasOne("EFDataAccess.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId");

                    b.Navigation("Album");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("EFDataAccess.Models.Album", b =>
                {
                    b.Navigation("songs");
                });

            modelBuilder.Entity("EFDataAccess.Models.Artist", b =>
                {
                    b.Navigation("Albums");
                });
#pragma warning restore 612, 618
        }
    }
}
