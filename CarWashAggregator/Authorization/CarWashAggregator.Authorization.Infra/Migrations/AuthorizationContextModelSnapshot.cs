﻿// <auto-generated />
using System;
using CarWashAggregator.Authorization.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarWashAggregator.Authorization.Infra.Migrations
{
    [DbContext(typeof(AuthorizationDbContext))]
    partial class AuthorizationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CarWashAggregator.Authorization.Domain.Entities.AuthorizationData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpireAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hash_password");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_login");

                    b.HasKey("Id");

                    b.ToTable("AuthorizationData");
                });
#pragma warning restore 612, 618
        }
    }
}
