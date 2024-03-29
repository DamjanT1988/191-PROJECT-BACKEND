﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _191_PROJECT_BACKEND.Migrations.Product
{
    public partial class InitialCreateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Product_title = table.Column<string>(type: "TEXT", nullable: true),
                    Ean_number = table.Column<string>(type: "TEXT", nullable: true),
                    Product_description = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: true),
                    Amount_storage = table.Column<int>(type: "INTEGER", nullable: true),
                    Expiration_date = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: true),
                    IsSwedish = table.Column<bool>(type: "INTEGER", nullable: true),
                    Image_path = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductModel");
        }
    }
}
