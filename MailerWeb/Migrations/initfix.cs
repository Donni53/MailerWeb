using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Migrations
{
    public partial class initfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Employees_ImapConfigurations_ImapSettingsId",
                "Employees");

            migrationBuilder.DropForeignKey(
                "FK_Employees_Settings_SettingsId",
                "Employees");

            migrationBuilder.DropForeignKey(
                "FK_Employees_SmtpConfigurations_SmtpSettingsId",
                "Employees");

            migrationBuilder.DropForeignKey(
                "FK_Signature_Employees_UserId",
                "Signature");

            migrationBuilder.DropPrimaryKey(
                "PK_Employees",
                "Employees");

            migrationBuilder.RenameTable(
                "Employees",
                newName: "Users");

            migrationBuilder.RenameIndex(
                "IX_Employees_SmtpSettingsId",
                table: "Users",
                newName: "IX_Users_SmtpSettingsId");

            migrationBuilder.RenameIndex(
                "IX_Employees_SettingsId",
                table: "Users",
                newName: "IX_Users_SettingsId");

            migrationBuilder.RenameIndex(
                "IX_Employees_ImapSettingsId",
                table: "Users",
                newName: "IX_Users_ImapSettingsId");

            migrationBuilder.AddPrimaryKey(
                "PK_Users",
                "Users",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_Signature_Users_UserId",
                "Signature",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Users_ImapConfigurations_ImapSettingsId",
                "Users",
                "ImapSettingsId",
                "ImapConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Users_Settings_SettingsId",
                "Users",
                "SettingsId",
                "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Users_SmtpConfigurations_SmtpSettingsId",
                "Users",
                "SmtpSettingsId",
                "SmtpConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Signature_Users_UserId",
                "Signature");

            migrationBuilder.DropForeignKey(
                "FK_Users_ImapConfigurations_ImapSettingsId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_Settings_SettingsId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_SmtpConfigurations_SmtpSettingsId",
                "Users");

            migrationBuilder.DropPrimaryKey(
                "PK_Users",
                "Users");

            migrationBuilder.RenameTable(
                "Users",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                "IX_Users_SmtpSettingsId",
                table: "Employees",
                newName: "IX_Employees_SmtpSettingsId");

            migrationBuilder.RenameIndex(
                "IX_Users_SettingsId",
                table: "Employees",
                newName: "IX_Employees_SettingsId");

            migrationBuilder.RenameIndex(
                "IX_Users_ImapSettingsId",
                table: "Employees",
                newName: "IX_Employees_ImapSettingsId");

            migrationBuilder.AddPrimaryKey(
                "PK_Employees",
                "Employees",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_Employees_ImapConfigurations_ImapSettingsId",
                "Employees",
                "ImapSettingsId",
                "ImapConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Employees_Settings_SettingsId",
                "Employees",
                "SettingsId",
                "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Employees_SmtpConfigurations_SmtpSettingsId",
                "Employees",
                "SmtpSettingsId",
                "SmtpConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Signature_Employees_UserId",
                "Signature",
                "UserId",
                "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}