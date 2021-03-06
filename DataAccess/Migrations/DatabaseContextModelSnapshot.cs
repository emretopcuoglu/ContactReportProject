﻿// <auto-generated />
using System;
using ContactReport.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContactReport.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ContactReport.Entity.CommInfo", b =>
                {
                    b.Property<Guid>("CommInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommInfoContent")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("CommInfoType");

                    b.Property<Guid?>("ContactId");

                    b.HasKey("CommInfoId");

                    b.HasIndex("ContactId");

                    b.ToTable("CommunicationInfo");
                });

            modelBuilder.Entity("ContactReport.Entity.Contact", b =>
                {
                    b.Property<Guid>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ContactId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("ContactReport.Entity.Report", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContactCount");

                    b.Property<Guid?>("ContactId");

                    b.Property<string>("Location");

                    b.Property<int?>("PhoneNumberCount");

                    b.Property<DateTime>("ReportDate");

                    b.Property<int>("ReportStatus");

                    b.HasKey("ReportId");

                    b.HasIndex("ContactId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("ContactReport.Entity.CommInfo", b =>
                {
                    b.HasOne("ContactReport.Entity.Contact", "Contact")
                        .WithMany("CommInfos")
                        .HasForeignKey("ContactId");
                });

            modelBuilder.Entity("ContactReport.Entity.Report", b =>
                {
                    b.HasOne("ContactReport.Entity.Contact", "Contact")
                        .WithMany("Reports")
                        .HasForeignKey("ContactId");
                });
#pragma warning restore 612, 618
        }
    }
}
