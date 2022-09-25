﻿// <auto-generated />
using System;
using Assignment2.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment2.DAL.Migrations
{
    [DbContext(typeof(MediaPlayerDbContext))]
    partial class MediaPlayerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.File", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("Albumid")
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

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int?>("Slideshowid")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("Albumid");

                    b.HasIndex("Slideshowid");

                    b.ToTable("Files");
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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("Slideshows");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.File", b =>
                {
                    b.HasOne("Assignment2.DAL.Models.Album", null)
                        .WithMany("files")
                        .HasForeignKey("Albumid");

                    b.HasOne("Assignment2.DAL.Models.Slideshow", null)
                        .WithMany("files")
                        .HasForeignKey("Slideshowid");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.Album", b =>
                {
                    b.Navigation("files");
                });

            modelBuilder.Entity("Assignment2.DAL.Models.Slideshow", b =>
                {
                    b.Navigation("files");
                });
#pragma warning restore 612, 618
        }
    }
}
