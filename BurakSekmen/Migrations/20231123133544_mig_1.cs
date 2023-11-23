using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSekmen.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AracKategoris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracYakıtTuru = table.Column<string>(type: "VarChar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AracKategoris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AracYaks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracYakıtTuru = table.Column<string>(type: "VarChar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AracYaks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Siteseos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sitebasligi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    siteanahtarkelime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sitelogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hakkimizda = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siteseos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracYakitTuruId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AracYakitId = table.Column<int>(type: "int", nullable: false),
                    AracKategoriId = table.Column<int>(type: "int", nullable: false),
                    AracAdı = table.Column<string>(type: "VarChar(60)", maxLength: 60, nullable: false),
                    Arackm = table.Column<string>(type: "VarChar(100)", maxLength: 100, nullable: false),
                    AracMotorTip = table.Column<string>(type: "VarChar(10)", maxLength: 10, nullable: false),
                    AracKoltukSayisi = table.Column<string>(type: "VarChar(2)", maxLength: 2, nullable: false),
                    AracValizSayisi = table.Column<string>(type: "VarChar(2)", maxLength: 2, nullable: false),
                    AracAcıklama = table.Column<string>(type: "VarChar(500)", maxLength: 500, nullable: false),
                    Resim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_AracKategoris_AracKategoriId",
                        column: x => x.AracKategoriId,
                        principalTable: "AracKategoris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_AracYaks_AracYakitId",
                        column: x => x.AracYakitId,
                        principalTable: "AracYaks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AracKategoriId",
                table: "Vehicles",
                column: "AracKategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AracYakitId",
                table: "Vehicles",
                column: "AracYakitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Siteseos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "AracKategoris");

            migrationBuilder.DropTable(
                name: "AracYaks");
        }
    }
}
