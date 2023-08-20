using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class upd_FileStorages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "FileStorages",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FileStorages",
                newName: "FileName");
        }
    }
}
