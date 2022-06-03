using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoEFCore.Migrations
{
    public partial class AjustandoNomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_CLienteId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CienteId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "CLienteId",
                table: "Pedidos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_CLienteId",
                table: "Pedidos",
                newName: "IX_Pedidos_ClienteId");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Pedidos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Pedidos",
                newName: "CLienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                newName: "IX_Pedidos_CLienteId");

            migrationBuilder.AlterColumn<int>(
                name: "CLienteId",
                table: "Pedidos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CienteId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_CLienteId",
                table: "Pedidos",
                column: "CLienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
