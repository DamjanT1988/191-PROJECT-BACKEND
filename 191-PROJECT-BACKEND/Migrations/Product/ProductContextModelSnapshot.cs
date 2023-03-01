﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _191_PROJECT_BACKEND.Data;

#nullable disable

namespace _191_PROJECT_BACKEND.Migrations.Product
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.14");

            modelBuilder.Entity("_191_PROJECT_BACKEND.Models.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Amount_storage")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ean_number")
                        .HasColumnType("TEXT");

                    b.Property<string>("Expiration_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image_path")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsSwedish")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("Product_description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Product_title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProductModel");
                });
#pragma warning restore 612, 618
        }
    }
}
