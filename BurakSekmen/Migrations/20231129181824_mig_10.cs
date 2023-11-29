using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSekmen.Migrations
{
    /// <inheritdoc />
    public partial class mig_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AracAdı",
                table: "Vehicles",
                type: "VarChar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(60)",
                oldMaxLength: 60);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AracAdı",
                table: "Vehicles",
                type: "VarChar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(250)",
                oldMaxLength: 250);
        }
    }
}
