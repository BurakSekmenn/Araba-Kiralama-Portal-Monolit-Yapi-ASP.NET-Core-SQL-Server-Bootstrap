using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSekmen.Migrations
{
    /// <inheritdoc />
    public partial class mig_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AracMarkas_aracMarkaId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "aracMarkaId",
                table: "Vehicles",
                newName: "AracMarkaId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_aracMarkaId",
                table: "Vehicles",
                newName: "IX_Vehicles_AracMarkaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AracMarkas_AracMarkaId",
                table: "Vehicles",
                column: "AracMarkaId",
                principalTable: "AracMarkas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AracMarkas_AracMarkaId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "AracMarkaId",
                table: "Vehicles",
                newName: "aracMarkaId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_AracMarkaId",
                table: "Vehicles",
                newName: "IX_Vehicles_aracMarkaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AracMarkas_aracMarkaId",
                table: "Vehicles",
                column: "aracMarkaId",
                principalTable: "AracMarkas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
