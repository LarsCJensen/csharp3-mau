﻿// <auto-generated />
using System;
using LoveYourBudget.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoveYourBudget.DAL.Migrations
{
    [DbContext(typeof(LoveYourBudgetDbContext))]
    [Migration("20221230101552_AddCategoryId")]
    partial class AddCategoryId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoveYourBudget.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Income")
                        .HasColumnType("int");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Budget");
                });

            modelBuilder.Entity("LoveYourBudget.BudgetRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int?>("BudgetId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BudgetRow");
                });

            modelBuilder.Entity("LoveYourBudget.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7103),
                            Name = "Groceries",
                            UpdatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7105)
                        },
                        new
                        {
                            Id = 2,
                            CreatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7111),
                            Name = "Phone",
                            UpdatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7113)
                        },
                        new
                        {
                            Id = 3,
                            CreatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7117),
                            Name = "Electricity",
                            UpdatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7119)
                        },
                        new
                        {
                            Id = 4,
                            CreatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7123),
                            Name = "Gas",
                            UpdatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7124)
                        },
                        new
                        {
                            Id = 5,
                            CreatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7128),
                            Name = "Broadband",
                            UpdatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7130)
                        },
                        new
                        {
                            Id = 6,
                            CreatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7134),
                            Name = "TV",
                            UpdatedTime = new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7136)
                        });
                });

            modelBuilder.Entity("LoveYourBudget.ExpenseRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int?>("BudgetId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ExpenseRows");
                });

            modelBuilder.Entity("LoveYourBudget.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("BudgetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("LockInPeriod")
                        .HasColumnType("datetime2");

                    b.Property<int>("Mortgage")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("LoveYourBudget.BudgetRow", b =>
                {
                    b.HasOne("LoveYourBudget.Budget", null)
                        .WithMany("BudgetRows")
                        .HasForeignKey("BudgetId");

                    b.HasOne("LoveYourBudget.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LoveYourBudget.ExpenseRow", b =>
                {
                    b.HasOne("LoveYourBudget.Budget", null)
                        .WithMany("ExpenseRows")
                        .HasForeignKey("BudgetId");

                    b.HasOne("LoveYourBudget.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LoveYourBudget.Loan", b =>
                {
                    b.HasOne("LoveYourBudget.Budget", null)
                        .WithMany("Loans")
                        .HasForeignKey("BudgetId");
                });

            modelBuilder.Entity("LoveYourBudget.Budget", b =>
                {
                    b.Navigation("BudgetRows");

                    b.Navigation("ExpenseRows");

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
