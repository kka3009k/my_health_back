using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class upd_Metrics_IntraocularPressure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntraocularPressure",
                table: "Metrics",
                newName: "IntraocularPressureRight");

            migrationBuilder.AddColumn<int>(
                name: "IntraocularPressureLeft",
                table: "Metrics",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntraocularPressureLeft",
                table: "Metrics");

            migrationBuilder.RenameColumn(
                name: "IntraocularPressureRight",
                table: "Metrics",
                newName: "IntraocularPressure");
        }
    }
}
