using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class upd_Analyzes_del_AnalysisType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzes_AnalysisTypes_AnalysisTypeID",
                table: "Analyzes");

            migrationBuilder.DropTable(
                name: "AnalysisTypes");

            migrationBuilder.DropIndex(
                name: "IX_Analyzes_AnalysisTypeID",
                table: "Analyzes");

            migrationBuilder.DropColumn(
                name: "AnalysisTypeID",
                table: "Analyzes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Analyzes",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Analyzes");

            migrationBuilder.AddColumn<int>(
                name: "AnalysisTypeID",
                table: "Analyzes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnalysisTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    Name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisTypes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyzes_AnalysisTypeID",
                table: "Analyzes",
                column: "AnalysisTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzes_AnalysisTypes_AnalysisTypeID",
                table: "Analyzes",
                column: "AnalysisTypeID",
                principalTable: "AnalysisTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
