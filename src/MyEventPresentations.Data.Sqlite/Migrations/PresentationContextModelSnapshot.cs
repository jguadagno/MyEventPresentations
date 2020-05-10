﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyEventPresentations.Data.Sqlite;

namespace MyEventPresentations.Data.Sqlite.Migrations
{
    [DbContext(typeof(PresentationContext))]
    partial class PresentationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("MyEventPresentations.Data.Sqlite.Models.Presentation", b =>
                {
                    b.Property<int>("PresentationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abstract")
                        .HasColumnType("TEXT");

                    b.Property<string>("MoreInfoUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("PowerpointUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceCodeRepositoryUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("VideoUri")
                        .HasColumnType("TEXT");

                    b.HasKey("PresentationId");

                    b.ToTable("Presentations");
                });

            modelBuilder.Entity("MyEventPresentations.Data.Sqlite.Models.ScheduledPresentation", b =>
                {
                    b.Property<int>("ScheduledPresentationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AttendeeCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PresentationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PresentationUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoomName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("VideoStorageUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("VideoUri")
                        .HasColumnType("TEXT");

                    b.HasKey("ScheduledPresentationId");

                    b.HasIndex("PresentationId");

                    b.ToTable("ScheduledPresentations");
                });

            modelBuilder.Entity("MyEventPresentations.Data.Sqlite.Models.ScheduledPresentation", b =>
                {
                    b.HasOne("MyEventPresentations.Data.Sqlite.Models.Presentation", "Presentation")
                        .WithMany("ScheduledPresentations")
                        .HasForeignKey("PresentationId");
                });
#pragma warning restore 612, 618
        }
    }
}
