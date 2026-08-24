using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Areneros.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LecturasArenero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Planta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Separador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tambor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arenero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturasArenero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LecturasHidro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Planta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bateria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hidrociclon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturasHidro", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LecturasArenero");

            migrationBuilder.DropTable(
                name: "LecturasHidro");
        }
    }
}
