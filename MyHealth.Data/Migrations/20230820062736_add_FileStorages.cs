using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_FileStorages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvatarFileID",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileStorages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorages", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarFileID",
                table: "Users",
                column: "AvatarFileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FileStorages_AvatarFileID",
                table: "Users",
                column: "AvatarFileID",
                principalTable: "FileStorages",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_FileStorages_AvatarFileID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "FileStorages");

            migrationBuilder.DropIndex(
                name: "IX_Users_AvatarFileID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarFileID",
                table: "Users");
        }
    }
}
