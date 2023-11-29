using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSekmen.Migrations
{
    /// <inheritdoc />
    public partial class mig_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "aracMarkaId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AracMarkas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aracmarka = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AracMarkas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_aracMarkaId",
                table: "Vehicles",
                column: "aracMarkaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AracMarkas_aracMarkaId",
                table: "Vehicles",
                column: "aracMarkaId",
                principalTable: "AracMarkas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AracMarkas_aracMarkaId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "AracMarkas");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_aracMarkaId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "aracMarkaId",
                table: "Vehicles");
        }
    }
}
