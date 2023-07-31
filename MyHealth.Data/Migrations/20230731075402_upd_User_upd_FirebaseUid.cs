using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class upd_User_upd_FirebaseUid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirebaseUID",
                table: "Users",
                newName: "FirebaseUid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirebaseUid",
                table: "Users",
                newName: "FirebaseUID");
        }
    }
}
