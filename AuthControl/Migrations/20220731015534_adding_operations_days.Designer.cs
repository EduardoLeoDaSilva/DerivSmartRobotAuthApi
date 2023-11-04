﻿// <auto-generated />
using System;
using AuthControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthControl.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220731015534_adding_operations_days")]
    partial class adding_operations_days
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AuthControl.Entities.OperationaDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<decimal>("EndBalance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("GainIncoming")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("gainIncoming");

                    b.Property<int>("QuantOperations")
                        .HasColumnType("int");

                    b.Property<string>("RobotsAccuracy")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("robotsAccuracy");

                    b.Property<decimal>("StartBalance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("operationDays", (string)null);
                });

            modelBuilder.Entity("AuthControl.Entities.Plans", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Robots")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Robots");

                    b.HasKey("Id");

                    b.ToTable("plans", (string)null);
                });

            modelBuilder.Entity("AuthControl.Entities.RetryQueue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("retries", (string)null);
                });

            modelBuilder.Entity("AuthControl.Entities.UserBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Robots")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("TokenDateDeadLine")
                        .HasColumnType("datetime");

                    b.Property<string>("TokensOAuth")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("AuthControl.Entities.OperationaDay", b =>
                {
                    b.HasOne("AuthControl.Entities.UserBase", "User")
                        .WithMany("OperationsDays")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AuthControl.Entities.UserBase", b =>
                {
                    b.Navigation("OperationsDays");
                });
#pragma warning restore 612, 618
        }
    }
}