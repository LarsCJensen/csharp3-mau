﻿// <auto-generated />
using System;
using Assignment2.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment2.DAL.Migrations
{
    [DbContext(typeof(MediaPlayerDbContext))]
    [Migration("20221005070418_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Assignment2.DAL.Models.Album", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfImages")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfVideos")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.AlbumFile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfImages")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfVideos")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("AlbumId");

                    b.ToTable("AlbumFiles");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.Slideshow", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Interval")
                        .HasColumnType("int");

                    b.Property<int>("LengthInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfImages")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfVideos")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("Slideshows");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.SlideshowFile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfImages")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfVideos")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("SlideshowId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("SlideshowId");

                    b.ToTable("SlideshowFiles");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.AlbumFile", b =>
                {
                    b.HasOne("Assignment2.DAL.Models.Album", "Album")
                        .WithMany("Files")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.SlideshowFile", b =>
                {
                    b.HasOne("Assignment2.DAL.Models.Slideshow", "Slideshow")
                        .WithMany("Files")
                        .HasForeignKey("SlideshowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Slideshow");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.Album", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.Slideshow", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
