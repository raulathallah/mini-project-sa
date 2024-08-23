﻿// <auto-generated />
using System;
using LibraryManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    [DbContext(typeof(LMSDbContext))]
    partial class LMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagementSystem.Core.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("bookid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("author");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("category");

                    b.Property<string>("DeleteReason")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("deletereason");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("isbn");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("language");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("publisher");

                    b.Property<int>("Stock")
                        .HasColumnType("integer")
                        .HasColumnName("stock");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("title");

                    b.HasKey("BookId");

                    b.ToTable("books");
                });

            modelBuilder.Entity("LibraryManagementSystem.Core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateOnly>("LibraryCardExpiredDate")
                        .HasMaxLength(255)
                        .HasColumnType("date")
                        .HasColumnName("librarycardexpireddate");

                    b.Property<string>("LibraryCardNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("librarycardnumber");

                    b.Property<string>("UserNotes")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("notes");

                    b.Property<string>("UserPosition")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("userposition");

                    b.Property<string>("UserPrivilage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("userprivilege");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("fname");

                    b.Property<string>("lName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("lname");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("LibraryManagementSystem.Domain.Models.Entities.BookUserTransactions", b =>
                {
                    b.Property<int>("BookUserTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("bookusertransactionid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookUserTransactionId"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("bookid");

                    b.Property<DateOnly>("DueDate")
                        .HasMaxLength(255)
                        .HasColumnType("date")
                        .HasColumnName("duedate");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("isbn");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("locationid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("BookUserTransactionId");

                    b.HasIndex("BookId");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("bookusertransactions");
                });

            modelBuilder.Entity("LibraryManagementSystem.Domain.Models.Entities.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("locationid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LocationId"));

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("locationname");

                    b.HasKey("LocationId");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("LibraryManagementSystem.Domain.Models.Entities.Stock", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("bookid");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("locationid");

                    b.Property<int>("StockCount")
                        .HasColumnType("integer")
                        .HasColumnName("stocks");

                    b.HasKey("BookId", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("stocks");
                });

            modelBuilder.Entity("LibraryManagementSystem.Domain.Models.Entities.BookUserTransactions", b =>
                {
                    b.HasOne("LibraryManagementSystem.Core.Models.Book", "BookIdNavigation")
                        .WithMany("BookUserTransactions")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagementSystem.Domain.Models.Entities.Location", "LocationIdNavigation")
                        .WithMany("BookUserTransactions")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagementSystem.Core.Models.User", "UserIdNavigation")
                        .WithMany("BookUserTransactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookIdNavigation");

                    b.Navigation("LocationIdNavigation");

                    b.Navigation("UserIdNavigation");
                });

            modelBuilder.Entity("LibraryManagementSystem.Domain.Models.Entities.Stock", b =>
                {
                    b.HasOne("LibraryManagementSystem.Core.Models.Book", "BookIdNavigation")
                        .WithMany("Stocks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagementSystem.Domain.Models.Entities.Location", "LocationIdNavigation")
                        .WithMany("Stocks")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookIdNavigation");

                    b.Navigation("LocationIdNavigation");
                });

            modelBuilder.Entity("LibraryManagementSystem.Core.Models.Book", b =>
                {
                    b.Navigation("BookUserTransactions");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("LibraryManagementSystem.Core.Models.User", b =>
                {
                    b.Navigation("BookUserTransactions");
                });

            modelBuilder.Entity("LibraryManagementSystem.Domain.Models.Entities.Location", b =>
                {
                    b.Navigation("BookUserTransactions");

                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
