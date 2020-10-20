using Microsoft.EntityFrameworkCore.Migrations;

namespace AppGM.Core.Migrations
{
    public partial class iiil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TIUtilizableEfecto_ModeloEfecto_EfectoIdEfecto",
                table: "TIUtilizableEfecto");

            migrationBuilder.DropForeignKey(
                name: "FK_TIUtilizableEfecto_ModeloUtilizable_UtilizableIdUtilizable",
                table: "TIUtilizableEfecto");

            migrationBuilder.DropIndex(
                name: "IX_TIUtilizableEfecto_EfectoIdEfecto",
                table: "TIUtilizableEfecto");

            migrationBuilder.DropIndex(
                name: "IX_TIUtilizableEfecto_UtilizableIdUtilizable",
                table: "TIUtilizableEfecto");

            migrationBuilder.DropColumn(
                name: "EfectoIdEfecto",
                table: "TIUtilizableEfecto");

            migrationBuilder.DropColumn(
                name: "UtilizableIdUtilizable",
                table: "TIUtilizableEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIUtilizableEfecto_IdEfecto",
                table: "TIUtilizableEfecto",
                column: "IdEfecto");

            migrationBuilder.AddForeignKey(
                name: "FK_TIUtilizableEfecto_ModeloEfecto_IdEfecto",
                table: "TIUtilizableEfecto",
                column: "IdEfecto",
                principalTable: "ModeloEfecto",
                principalColumn: "IdEfecto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TIUtilizableEfecto_ModeloUtilizable_IdUtilizable",
                table: "TIUtilizableEfecto",
                column: "IdUtilizable",
                principalTable: "ModeloUtilizable",
                principalColumn: "IdUtilizable",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TIUtilizableEfecto_ModeloEfecto_IdEfecto",
                table: "TIUtilizableEfecto");

            migrationBuilder.DropForeignKey(
                name: "FK_TIUtilizableEfecto_ModeloUtilizable_IdUtilizable",
                table: "TIUtilizableEfecto");

            migrationBuilder.DropIndex(
                name: "IX_TIUtilizableEfecto_IdEfecto",
                table: "TIUtilizableEfecto");

            migrationBuilder.AddColumn<int>(
                name: "EfectoIdEfecto",
                table: "TIUtilizableEfecto",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilizableIdUtilizable",
                table: "TIUtilizableEfecto",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIUtilizableEfecto_EfectoIdEfecto",
                table: "TIUtilizableEfecto",
                column: "EfectoIdEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIUtilizableEfecto_UtilizableIdUtilizable",
                table: "TIUtilizableEfecto",
                column: "UtilizableIdUtilizable");

            migrationBuilder.AddForeignKey(
                name: "FK_TIUtilizableEfecto_ModeloEfecto_EfectoIdEfecto",
                table: "TIUtilizableEfecto",
                column: "EfectoIdEfecto",
                principalTable: "ModeloEfecto",
                principalColumn: "IdEfecto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TIUtilizableEfecto_ModeloUtilizable_UtilizableIdUtilizable",
                table: "TIUtilizableEfecto",
                column: "UtilizableIdUtilizable",
                principalTable: "ModeloUtilizable",
                principalColumn: "IdUtilizable",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
