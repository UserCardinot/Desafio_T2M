using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioSGP.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projetos",
                table: "Projetos");

            migrationBuilder.RenameTable(
                name: "Projetos",
                newName: "projetos");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Prazo",
                table: "projetos",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_projetos",
                table: "projetos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_projetos_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId",
                principalTable: "projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_projetos_ProjetoId",
                table: "Tarefas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projetos",
                table: "projetos");

            migrationBuilder.RenameTable(
                name: "projetos",
                newName: "Projetos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Prazo",
                table: "Projetos",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projetos",
                table: "Projetos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
