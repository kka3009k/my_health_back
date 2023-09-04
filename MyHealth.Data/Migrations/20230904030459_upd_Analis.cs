using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class upd_Analis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzes_Laboratories_LaboratoryID",
                table: "Analyzes");

            migrationBuilder.AlterColumn<int>(
                name: "LaboratoryID",
                table: "Analyzes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzes_Laboratories_LaboratoryID",
                table: "Analyzes",
                column: "LaboratoryID",
                principalTable: "Laboratories",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzes_Laboratories_LaboratoryID",
                table: "Analyzes");

            migrationBuilder.AlterColumn<int>(
                name: "LaboratoryID",
                table: "Analyzes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzes_Laboratories_LaboratoryID",
                table: "Analyzes",
                column: "LaboratoryID",
                principalTable: "Laboratories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
