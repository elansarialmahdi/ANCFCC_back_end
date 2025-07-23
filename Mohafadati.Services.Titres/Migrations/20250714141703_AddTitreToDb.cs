using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mohafadati.Services.Titres.Migrations
{
    /// <inheritdoc />
    public partial class AddTitreToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Titres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConservationFonciere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTitre = table.Column<int>(type: "int", nullable: false),
                    Indice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndiceSpecial = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titres", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Titres");
        }
    }
}
