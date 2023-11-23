using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSekmen.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AracKategoris_AracKategoriId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AracYaks_AracYakitId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "AracYakitId",
                table: "Vehicles",
                newName: "AracYakıId");

            migrationBuilder.RenameColumn(
                name: "AracKategoriId",
                table: "Vehicles",
                newName: "AracKategorId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_AracYakitId",
                table: "Vehicles",
                newName: "IX_Vehicles_AracYakıId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_AracKategoriId",
                table: "Vehicles",
                newName: "IX_Vehicles_AracKategorId");

            migrationBuilder.RenameColumn(
                name: "AracYakıtTuru",
                table: "AracKategoris",
                newName: "AracKategoriAdi");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AracKategoris_AracKategorId",
                table: "Vehicles",
                column: "AracKategorId",
                principalTable: "AracKategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AracYaks_AracYakıId",
                table: "Vehicles",
                column: "AracYakıId",
                principalTable: "AracYaks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AracKategoris_AracKategorId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AracYaks_AracYakıId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "AracYakıId",
                table: "Vehicles",
                newName: "AracYakitId");

            migrationBuilder.RenameColumn(
                name: "AracKategorId",
                table: "Vehicles",
                newName: "AracKategoriId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_AracYakıId",
                table: "Vehicles",
                newName: "IX_Vehicles_AracYakitId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_AracKategorId",
                table: "Vehicles",
                newName: "IX_Vehicles_AracKategoriId");

            migrationBuilder.RenameColumn(
                name: "AracKategoriAdi",
                table: "AracKategoris",
                newName: "AracYakıtTuru");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AracKategoris_AracKategoriId",
                table: "Vehicles",
                column: "AracKategoriId",
                principalTable: "AracKategoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AracYaks_AracYakitId",
                table: "Vehicles",
                column: "AracYakitId",
                principalTable: "AracYaks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
