﻿// <auto-generated />
using System;
using GameEngine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameEngine.Migrations
{
    [DbContext(typeof(DbModel))]
    partial class DbModelModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("GameEngine.Classes.Rules", b =>
                {
                    b.Property<int>("RulesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("InitialSixRuleEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfPlayers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PiecesPerPlayer")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SaveGameId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ThrowAgainOnSixEnabled")
                        .HasColumnType("INTEGER");

                    b.HasKey("RulesId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("GameEngine.Classes.SaveGame", b =>
                {
                    b.Property<int>("SaveGameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentPlayer")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.HasKey("SaveGameId");

                    b.ToTable("SaveGames");
                });

            modelBuilder.Entity("GameEngine.GamePiece", b =>
                {
                    b.Property<int>("GamePieceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Col")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasFinished")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPlacedOnBoard")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OnInnerPath")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Row")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SaveGameId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GamePieceId");

                    b.ToTable("Pieces");
                });

            modelBuilder.Entity("GameEngine.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Col")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Row")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SaveGameId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
