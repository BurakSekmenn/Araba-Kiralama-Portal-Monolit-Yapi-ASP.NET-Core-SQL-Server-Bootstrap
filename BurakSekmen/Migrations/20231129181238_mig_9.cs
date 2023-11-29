using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSekmen.Migrations
{
    /// <inheritdoc />
    public partial class mig_9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AracYakıtTuru",
                table: "AracYaks",
                type: "VarChar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(10)",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AracYakıtTuru",
                table: "AracYaks",
                type: "VarChar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(30)",
                oldMaxLength: 30);
        }
    }
}
