using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS_V2.Data.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Organisations");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Staffs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumber",
                table: "Organisations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebAddress",
                table: "Organisations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "WebAddress",
                table: "Organisations");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Staffs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Organisations",
                type: "text",
                nullable: true);
        }
    }
}
