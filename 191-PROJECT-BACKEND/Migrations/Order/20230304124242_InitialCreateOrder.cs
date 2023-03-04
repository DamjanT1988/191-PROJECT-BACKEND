using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _191_PROJECT_BACKEND.Migrations.Order
{
    public partial class InitialCreateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    order = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    telephone = table.Column<string>(type: "TEXT", nullable: true),
                    company_name = table.Column<string>(type: "TEXT", nullable: true),
                    company_org = table.Column<string>(type: "TEXT", nullable: true),
                    company_adress = table.Column<string>(type: "TEXT", nullable: true),
                    contact_name = table.Column<string>(type: "TEXT", nullable: true),
                    status = table.Column<string>(type: "TEXT", nullable: true),
                    internal_note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderModel");
        }
    }
}
