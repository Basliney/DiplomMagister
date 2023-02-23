﻿// <auto-generated />
using System;
using DiplomMagister.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomMagister.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230206205923_setNullableToToken")]
    partial class setNullableToToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DiplomMagister.Classes.Client.UserClient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<int?>("TokenId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TokenId");

                    b.ToTable("UserClients");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EncodedJwt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("NotBefore")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("JWT_Example_ASP.Models.UserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("UserClientId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserClientId");

                    b.ToTable("UsersData");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Client.UserClient", b =>
                {
                    b.HasOne("DiplomMagister.Classes.Token", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId");

                    b.OwnsOne("DiplomMagister.Classes.Client.UserInfo", "Userinfo", b1 =>
                        {
                            b1.Property<string>("UserClientId")
                                .HasColumnType("text");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Lastname")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Middlename")
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("UserClientId");

                            b1.ToTable("UserClients");

                            b1.WithOwner()
                                .HasForeignKey("UserClientId");
                        });

                    b.Navigation("Token");

                    b.Navigation("Userinfo")
                        .IsRequired();
                });

            modelBuilder.Entity("JWT_Example_ASP.Models.UserData", b =>
                {
                    b.HasOne("DiplomMagister.Classes.Client.UserClient", "UserClient")
                        .WithMany()
                        .HasForeignKey("UserClientId");

                    b.Navigation("UserClient");
                });
#pragma warning restore 612, 618
        }
    }
}
