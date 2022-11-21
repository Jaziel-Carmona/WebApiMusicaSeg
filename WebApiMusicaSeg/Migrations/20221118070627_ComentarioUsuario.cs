using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMusicaSeg.Migrations
{
    public partial class ComentarioUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Albums",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UsuarioId",
                table: "Albums",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_UsuarioId",
                table: "Albums",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_UsuarioId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_UsuarioId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Albums");
        }
    }
}
