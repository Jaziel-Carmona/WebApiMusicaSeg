using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMusicaSeg.Migrations
{
    public partial class ArtistaCancion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtistaCancion",
                columns: table => new
                {
                    ArtistaId = table.Column<int>(type: "int", nullable: false),
                    CancionId = table.Column<int>(type: "int", nullable: false),
                    Orden_Lanzamiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistaCancion", x => new { x.ArtistaId, x.CancionId });
                    table.ForeignKey(
                        name: "FK_ArtistaCancion_Artistas_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "Artistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistaCancion_Canciones_CancionId",
                        column: x => x.CancionId,
                        principalTable: "Canciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistaCancion_CancionId",
                table: "ArtistaCancion",
                column: "CancionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistaCancion");
        }
    }
}
