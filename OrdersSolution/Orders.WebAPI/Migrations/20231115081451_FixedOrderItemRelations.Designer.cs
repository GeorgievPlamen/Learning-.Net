﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orders.WebAPI.AppDbContext;

#nullable disable

namespace Orders.WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231115081451_FixedOrderItemRelations")]
    partial class FixedOrderItemRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Orders.WebAPI.Enteties.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.ToTable("Orders", (string)null);

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                            CustomerName = "John Doe",
                            OrderDate = new DateTime(2023, 11, 15, 10, 14, 50, 939, DateTimeKind.Local).AddTicks(831),
                            OrderNumber = "ORD001",
                            TotalAmount = 66.5
                        },
                        new
                        {
                            OrderId = new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                            CustomerName = "Jane Smith",
                            OrderDate = new DateTime(2023, 11, 15, 10, 14, 50, 939, DateTimeKind.Local).AddTicks(861),
                            OrderNumber = "ORD002",
                            TotalAmount = 225.80000000000001
                        });
                });

            modelBuilder.Entity("Orders.WebAPI.Enteties.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double?>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<double?>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);

                    b.HasData(
                        new
                        {
                            OrderItemId = new Guid("d20882df-7fca-4ee8-88bb-37d2fc75e63f"),
                            OrderId = new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                            ProductName = "Product A",
                            Quantity = 2,
                            TotalPrice = 20.0,
                            UnitPrice = 10.0
                        },
                        new
                        {
                            OrderItemId = new Guid("2e27b6a4-469d-4d7f-8b8b-54af129675fd"),
                            OrderId = new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                            ProductName = "Product B",
                            Quantity = 3,
                            TotalPrice = 46.5,
                            UnitPrice = 15.5
                        },
                        new
                        {
                            OrderItemId = new Guid("24d71ac2-0a9c-4914-9fd3-13bc25d42694"),
                            OrderId = new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                            ProductName = "Product C",
                            Quantity = 7,
                            TotalPrice = 25.0,
                            UnitPrice = 25.399999999999999
                        },
                        new
                        {
                            OrderItemId = new Guid("ac90b8bc-349d-43fd-87a6-6a7ed8057697"),
                            OrderId = new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                            ProductName = "Product D",
                            Quantity = 4,
                            TotalPrice = 25.0,
                            UnitPrice = 12.0
                        });
                });

            modelBuilder.Entity("Orders.WebAPI.Enteties.OrderItem", b =>
                {
                    b.HasOne("Orders.WebAPI.Enteties.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
