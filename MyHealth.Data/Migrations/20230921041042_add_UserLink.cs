using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_UserLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLinks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MainUserID = table.Column<int>(type: "integer", nullable: false),
                    SecondaryUserID = table.Column<int>(type: "integer", nullable: false),
                    UserLinkTypeID = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLinks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserLinks_Users_MainUserID",
                        column: x => x.MainUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLinks_Users_SecondaryUserID",
                        column: x => x.SecondaryUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLinks_Users_UserLinkTypeID",
                        column: x => x.UserLinkTypeID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLinkTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLinkTypes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLinks_MainUserID",
                table: "UserLinks",
                column: "MainUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLinks_SecondaryUserID",
                table: "UserLinks",
                column: "SecondaryUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLinks_UserLinkTypeID",
                table: "UserLinks",
                column: "UserLinkTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLinks");

            migrationBuilder.DropTable(
                name: "UserLinkTypes");
        }
    }
}
