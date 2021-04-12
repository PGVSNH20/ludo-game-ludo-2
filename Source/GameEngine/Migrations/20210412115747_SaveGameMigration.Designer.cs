﻿// <auto-generated />
using System;
using GameEngine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameEngine.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20210412115747_SaveGameMigration")]
    partial class SaveGameMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameEngine.Classes.SaveGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("GamePiece1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GamePiece2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GamePiece3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GamePiece4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GameInProgress");
                });
#pragma warning restore 612, 618
        }
    }
}
