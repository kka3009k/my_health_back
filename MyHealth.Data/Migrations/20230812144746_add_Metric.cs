using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_Metric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: true),
                    Saturation = table.Column<int>(type: "integer", nullable: true),
                    Pulse = table.Column<int>(type: "integer", nullable: true),
                    IntraocularPressure = table.Column<int>(type: "integer", nullable: true),
                    AbdominalGirth = table.Column<double>(type: "double precision", nullable: true),
                    ArterialPressureUpper = table.Column<int>(type: "integer", nullable: true),
                    ArterialPressureLower = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Metrics_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_UserID",
                table: "Metrics",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metrics");
        }
    }
}
