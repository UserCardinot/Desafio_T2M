using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioSGP.Migrations
{
    /// <inheritdoc />
    public partial class NovaAtualizacaoTabelaProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_projetos_ProjetoId",
                table: "Tarefa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projetos",
                table: "projetos");

            migrationBuilder.RenameTable(
                name: "projetos",
                newName: "nova_tabela_projetos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_nova_tabela_projetos",
                table: "nova_tabela_projetos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_nova_tabela_projetos_ProjetoId",
                table: "Tarefa",
                column: "ProjetoId",
                principalTable: "nova_tabela_projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_nova_tabela_projetos_ProjetoId",
                table: "Tarefa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_nova_tabela_projetos",
                table: "nova_tabela_projetos");

            migrationBuilder.RenameTable(
                name: "nova_tabela_projetos",
                newName: "projetos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projetos",
                table: "projetos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_projetos_ProjetoId",
                table: "Tarefa",
                column: "ProjetoId",
                principalTable: "projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
