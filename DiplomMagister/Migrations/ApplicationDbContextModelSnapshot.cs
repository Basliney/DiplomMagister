﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DiplomMagister.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomMagister.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasKey("Id");

                    b.ToTable("UserClients");
                });

            modelBuilder.Entity("DiplomMagister.Classes.DTOs.TagDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<string>("UserClientId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("UserClientId");

                    b.ToTable("TagDTO");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.BasicQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string[]>("Answers")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Essence")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IndexOfTrue")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalScore")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BasicQuestions");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.Statistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Completed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Mark")
                        .HasColumnType("integer");

                    b.Property<int>("Persent")
                        .HasColumnType("integer");

                    b.Property<int>("TestId")
                        .HasColumnType("integer");

                    b.Property<string>("UserClientId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("UserClientId");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserClientId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserClientId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Questions")
                        .HasColumnType("text[]");

                    b.Property<int>("TestInfoId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Visibility")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TestInfoId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.TestInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Mark")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("TestInfo");
                });

            modelBuilder.Entity("JWT_Example_ASP.Models.ProfileSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("ProfileSettings");
                });

            modelBuilder.Entity("TagTest", b =>
                {
                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.Property<int>("TestsId")
                        .HasColumnType("integer");

                    b.HasKey("TagsId", "TestsId");

                    b.HasIndex("TestsId");

                    b.ToTable("TagTest");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Client.UserClient", b =>
                {
                    b.OwnsOne("DiplomMagister.Classes.Client.ProfileInformation", "ProfileInformation", b1 =>
                        {
                            b1.Property<string>("UserClientId")
                                .HasColumnType("text");

                            b1.Property<DateTime?>("EditingDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Image")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<DateTime?>("LastEnterance")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("Lastname")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Mail")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int>("Privacy")
                                .HasColumnType("integer");

                            b1.HasKey("UserClientId");

                            b1.ToTable("UserClients");

                            b1.WithOwner()
                                .HasForeignKey("UserClientId");
                        });

                    b.Navigation("ProfileInformation")
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomMagister.Classes.DTOs.TagDTO", b =>
                {
                    b.HasOne("DiplomMagister.Classes.Tests.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomMagister.Classes.Client.UserClient", "UserClient")
                        .WithMany()
                        .HasForeignKey("UserClientId");

                    b.Navigation("Tag");

                    b.Navigation("UserClient");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.Statistics", b =>
                {
                    b.HasOne("DiplomMagister.Classes.Tests.Test", "Test")
                        .WithMany("Statistics")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomMagister.Classes.Client.UserClient", "UserClient")
                        .WithMany("Statistics")
                        .HasForeignKey("UserClientId");

                    b.Navigation("Test");

                    b.Navigation("UserClient");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.Tag", b =>
                {
                    b.HasOne("DiplomMagister.Classes.Client.UserClient", null)
                        .WithMany("Favorites")
                        .HasForeignKey("UserClientId");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.Test", b =>
                {
                    b.HasOne("DiplomMagister.Classes.Client.UserClient", "Creator")
                        .WithMany("CreatedTests")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomMagister.Classes.Tests.TestInfo", "TestInfo")
                        .WithMany()
                        .HasForeignKey("TestInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("TestInfo");
                });

            modelBuilder.Entity("JWT_Example_ASP.Models.ProfileSettings", b =>
                {
                    b.HasOne("DiplomMagister.Classes.Client.UserClient", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TagTest", b =>
                {
                    b.HasOne("DiplomMagister.Classes.Tests.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomMagister.Classes.Tests.Test", null)
                        .WithMany()
                        .HasForeignKey("TestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomMagister.Classes.Client.UserClient", b =>
                {
                    b.Navigation("CreatedTests");

                    b.Navigation("Favorites");

                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("DiplomMagister.Classes.Tests.Test", b =>
                {
                    b.Navigation("Statistics");
                });
#pragma warning restore 612, 618
        }
    }
}
