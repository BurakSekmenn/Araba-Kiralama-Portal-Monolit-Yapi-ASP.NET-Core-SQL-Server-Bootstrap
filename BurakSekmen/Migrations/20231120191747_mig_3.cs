using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSekmen.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siteseos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sitebasligi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    siteanahtarkelime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sitelogo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siteseos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Siteseos");
        }
    }
}
