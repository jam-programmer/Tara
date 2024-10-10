﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(SqlServerContext))]
    partial class SqlServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("CodeGenerator")
                .StartsAt(100L)
                .IncrementsBy(2)
                .HasMax(9223372036854775807L);

            modelBuilder.Entity("Domain.Entities.MerchantAccessEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("accessCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("terminalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("terminalTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("merchantCode");

                    b.HasIndex("terminalCode");

                    b.ToTable("MerchantAccess", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderDetailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Count")
                        .HasColumnType("real");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Fee")
                        .HasColumnType("bigint");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Made")
                        .HasColumnType("int");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerFullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("NEXT VALUE FOR CodeGenerator");

                    b.Property<string>("OrderDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.Property<long>("ValueAddedTax")
                        .HasColumnType("bigint");

                    b.Property<string>("WalletBarcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderTrackingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Code")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TraceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("OrderTracking", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TaraPurchaseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PurchaseType")
                        .HasColumnType("int");

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("doTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("referenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("terminal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("traceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("TaraPurchase", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderDetailEntity", b =>
                {
                    b.HasOne("Domain.Entities.OrderEntity", "order")
                        .WithMany("Details")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");
                });

            modelBuilder.Entity("Domain.Entities.OrderTrackingEntity", b =>
                {
                    b.HasOne("Domain.Entities.OrderEntity", "Order")
                        .WithOne("OrderTracking")
                        .HasForeignKey("Domain.Entities.OrderTrackingEntity", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.TaraPurchaseEntity", b =>
                {
                    b.HasOne("Domain.Entities.OrderEntity", "Order")
                        .WithMany("TaraPurchases")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.OrderEntity", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("OrderTracking");

                    b.Navigation("TaraPurchases");
                });
#pragma warning restore 612, 618
        }
    }
}
