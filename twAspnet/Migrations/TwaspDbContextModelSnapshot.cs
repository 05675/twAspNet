﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using twAspnet.Models;

namespace twAspnet.Migrations
{
    [DbContext(typeof(TwaspDbContext))]
    partial class TwaspDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("twAspnet.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Comment")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Favoritedate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Twid")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("twAspnet.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mail")
                        .HasColumnType("TEXT");

                    b.Property<int>("Name")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Regdate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
