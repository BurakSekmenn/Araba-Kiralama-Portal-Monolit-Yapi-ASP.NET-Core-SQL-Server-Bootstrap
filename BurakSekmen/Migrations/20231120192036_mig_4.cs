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
            migrationBuilder.AddColumn<string>(
                name: "hakkimizda",
                table: "Siteseos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hakkimizda",
                table: "Siteseos");
        }
    }
}
