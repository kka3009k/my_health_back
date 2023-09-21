using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class upd_UserLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLinks_Users_UserLinkTypeID",
                table: "UserLinks");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLinks_UserLinkTypes_UserLinkTypeID",
                table: "UserLinks",
                column: "UserLinkTypeID",
                principalTable: "UserLinkTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLinks_UserLinkTypes_UserLinkTypeID",
                table: "UserLinks");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLinks_Users_UserLinkTypeID",
                table: "UserLinks",
                column: "UserLinkTypeID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
