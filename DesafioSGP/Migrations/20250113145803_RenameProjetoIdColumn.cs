using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioSGP.Migrations
{
    /// <inheritdoc />
    public partial class RenameProjetoIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "projetos",
                newName: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "projetos",
                newName: "Id");
        }
    }
}
