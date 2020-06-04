using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStore.Catalogo.Data.Migrations
{
    public partial class agum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altura",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Dimensoes_Id",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Largura",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Profundidade",
                table: "Produtos");

            migrationBuilder.AddColumn<Guid>(
                name: "DimensoesId",
                table: "Produtos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dimensoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Altura = table.Column<decimal>(nullable: false),
                    Largura = table.Column<decimal>(nullable: false),
                    Profundidade = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensoes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_DimensoesId",
                table: "Produtos",
                column: "DimensoesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Dimensoes_DimensoesId",
                table: "Produtos",
                column: "DimensoesId",
                principalTable: "Dimensoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Dimensoes_DimensoesId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Dimensoes");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_DimensoesId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "DimensoesId",
                table: "Produtos");

            migrationBuilder.AddColumn<int>(
                name: "Altura",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Dimensoes_Id",
                table: "Produtos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Largura",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Profundidade",
                table: "Produtos",
                type: "int",
                nullable: true);
        }
    }
}
