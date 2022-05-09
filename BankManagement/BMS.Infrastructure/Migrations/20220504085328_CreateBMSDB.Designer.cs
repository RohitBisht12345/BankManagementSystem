﻿// <auto-generated />
using System;
using BMS.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BMS.Infrastructure.Migrations
{
    [DbContext(typeof(BmsContext))]
    [Migration("20220504085328_CreateBMSDB")]
    partial class CreateBMSDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BMS.Models.Entities.Accounts", b =>
                {
                    b.Property<Guid>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BMS.Models.Entities.Loans", b =>
                {
                    b.Property<Guid>("LoanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InterestRate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoanAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoanDuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoanType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanID");

                    b.ToTable("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
