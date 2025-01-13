using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioSGP.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "projetos",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Prazo",
                table: "projetos",
                type: "TIMESTAMP",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "projetos",
                newName: "ProjetoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Prazo",
                table: "projetos",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP",
                oldNullable: true);
        }
    }
}
