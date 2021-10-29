﻿// <auto-generated />
using System;
using CarWashAggregator.Orders.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarWashAggregator.Orders.Infra.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20211005140130_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CarWashAggregator.Orders.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DateReservation")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("reservation_at");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("wash_price");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("CarWashId")
                        .HasColumnType("uuid")
                        .HasColumnName("carwash_Id");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CarWashAggregator.Orders.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name_of_status");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("CarWashAggregator.Orders.Domain.Entities.OrderStatus", b =>
                {
                    b.HasOne("CarWashAggregator.Orders.Domain.Entities.Order", "Order")
                        .WithOne("carWashStatus")
                        .HasForeignKey("CarWashAggregator.Orders.Domain.Entities.OrderStatus", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("CarWashAggregator.Orders.Domain.Entities.Order", b =>
                {
                    b.Navigation("carWashStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
