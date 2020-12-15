using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactReport.DataAccess.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    Company = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationInfo",
                columns: table => new
                {
                    CommInfoId = table.Column<Guid>(nullable: false),
                    CommInfoType = table.Column<int>(nullable: false),
                    CommInfoContent = table.Column<string>(maxLength: 200, nullable: false),
                    ContactId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationInfo", x => x.CommInfoId);
                    table.ForeignKey(
                        name: "FK_CommunicationInfo_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(nullable: false),
                    ReportDate = table.Column<DateTime>(nullable: false),
                    ReportStatus = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    ContactCount = table.Column<int>(nullable: true),
                    PhoneNumberCount = table.Column<int>(nullable: true),
                    ContactId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Report_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationInfo_ContactId",
                table: "CommunicationInfo",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ContactId",
                table: "Report",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunicationInfo");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
