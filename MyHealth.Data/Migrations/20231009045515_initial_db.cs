using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileStorages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Laboratories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhoneVerificationData",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Phone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    OTP = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Сonfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneVerificationData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserLinkTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLinkTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Phone = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Patronymic = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: true),
                    FirebaseUid = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    INN = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    Blood = table.Column<int>(type: "integer", nullable: true),
                    RhFactor = table.Column<int>(type: "integer", nullable: true),
                    AvatarFileID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_FileStorages_AvatarFileID",
                        column: x => x.AvatarFileID,
                        principalTable: "FileStorages",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Analyzes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    LaboratoryID = table.Column<Guid>(type: "uuid", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    ExtraInfo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyzes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Analyzes_Laboratories_LaboratoryID",
                        column: x => x.LaboratoryID,
                        principalTable: "Laboratories",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Analyzes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: true),
                    Saturation = table.Column<int>(type: "integer", nullable: true),
                    Pulse = table.Column<int>(type: "integer", nullable: true),
                    IntraocularPressureLeft = table.Column<int>(type: "integer", nullable: true),
                    IntraocularPressureRight = table.Column<int>(type: "integer", nullable: true),
                    AbdominalGirth = table.Column<double>(type: "double precision", nullable: true),
                    ArterialPressureUpper = table.Column<int>(type: "integer", nullable: true),
                    ArterialPressureLower = table.Column<int>(type: "integer", nullable: true),
                    DateFilling = table.Column<DateTime>(type: "date", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Symptoms_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLinks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    MainUserID = table.Column<Guid>(type: "uuid", nullable: false),
                    SecondaryUserID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserLinkTypeID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLinks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserLinks_UserLinkTypes_UserLinkTypeID",
                        column: x => x.UserLinkTypeID,
                        principalTable: "UserLinkTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                });

            migrationBuilder.CreateTable(
                name: "AnalysisFiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    AnalysisID = table.Column<Guid>(type: "uuid", nullable: false),
                    FileID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AnalysisFiles_Analyzes_AnalysisID",
                        column: x => x.AnalysisID,
                        principalTable: "Analyzes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalysisFiles_FileStorages_FileID",
                        column: x => x.FileID,
                        principalTable: "FileStorages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SymptomFiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomID = table.Column<Guid>(type: "uuid", nullable: false),
                    FileID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SymptomFiles_FileStorages_FileID",
                        column: x => x.FileID,
                        principalTable: "FileStorages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SymptomFiles_Symptoms_SymptomID",
                        column: x => x.SymptomID,
                        principalTable: "Symptoms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisFiles_AnalysisID",
                table: "AnalysisFiles",
                column: "AnalysisID");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisFiles_FileID",
                table: "AnalysisFiles",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_Analyzes_LaboratoryID",
                table: "Analyzes",
                column: "LaboratoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Analyzes_UserID",
                table: "Analyzes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_UserID",
                table: "Metrics",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomFiles_FileID",
                table: "SymptomFiles",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomFiles_SymptomID",
                table: "SymptomFiles",
                column: "SymptomID");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_UserID",
                table: "Symptoms",
                column: "UserID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarFileID",
                table: "Users",
                column: "AvatarFileID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisFiles");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "PhoneVerificationData");

            migrationBuilder.DropTable(
                name: "SymptomFiles");

            migrationBuilder.DropTable(
                name: "UserLinks");

            migrationBuilder.DropTable(
                name: "Analyzes");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "UserLinkTypes");

            migrationBuilder.DropTable(
                name: "Laboratories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FileStorages");
        }
    }
}
