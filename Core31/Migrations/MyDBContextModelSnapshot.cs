﻿// <auto-generated />
using System;
using Core31.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core31.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SDHC.Common.EntityCore.Models.BaseContent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DisplayOrder")
                        .HasColumnType("bigint");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Contents");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseContent");
                });

            modelBuilder.Entity("SDHC.Common.EntityCore.Models.BaseSelect", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Selects");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseSelect");
                });

            modelBuilder.Entity("Core31.Models.BaseContentModel", b =>
                {
                    b.HasBaseType("SDHC.Common.EntityCore.Models.BaseContent");

                    b.HasDiscriminator().HasValue("BaseContentModel");
                });

            modelBuilder.Entity("Core31.Models.BaseSelectModel", b =>
                {
                    b.HasBaseType("SDHC.Common.EntityCore.Models.BaseSelect");

                    b.HasDiscriminator().HasValue("BaseSelectModel");
                });

            modelBuilder.Entity("SDHC.Common.EntityCore.Models.BaseContent", b =>
                {
                    b.HasOne("SDHC.Common.EntityCore.Models.BaseContent", "ThisParent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
