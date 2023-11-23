using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSekmen.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ArabaYatagi",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Arackiti",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CocukKoltugu",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmniyetKemeri",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Gps",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Klima",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Music",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SesGirisi",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "arababilgisayarı",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "bagaj",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "bluetooth",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "dolab",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ilkyardımcantası",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "klimakontrol",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "uzaktankitleme",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabaYatagi",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Arackiti",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CocukKoltugu",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "EmniyetKemeri",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Gps",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Klima",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Music",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "SesGirisi",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "arababilgisayarı",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "bagaj",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "bluetooth",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "dolab",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ilkyardımcantası",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "klimakontrol",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "uzaktankitleme",
                table: "Vehicles");
        }
    }
}
