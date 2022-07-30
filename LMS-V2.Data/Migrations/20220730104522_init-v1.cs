using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS_V2.Data.Migrations
{
    public partial class initv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    OrganisationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    WebAddress = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.OrganisationId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationsStaff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    OrganisationId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationsStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisationsStaff_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "OrganisationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationsStaff_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationsStaff_OrganisationId",
                table: "OrganisationsStaff",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationsStaff_StaffId",
                table: "OrganisationsStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_Email",
                table: "Staffs",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganisationsStaff");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "Staffs");
        }
    }
}
