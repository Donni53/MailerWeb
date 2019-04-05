using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Migrations
{
    public partial class initfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ImapConfigurations_ImapSettingsId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Settings_SettingsId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SmtpConfigurations_SmtpSettingsId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Signature_Employees_UserId",
                table: "Signature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SmtpSettingsId",
                table: "Users",
                newName: "IX_Users_SmtpSettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SettingsId",
                table: "Users",
                newName: "IX_Users_SettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ImapSettingsId",
                table: "Users",
                newName: "IX_Users_ImapSettingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Signature_Users_UserId",
                table: "Signature",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ImapConfigurations_ImapSettingsId",
                table: "Users",
                column: "ImapSettingsId",
                principalTable: "ImapConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users",
                column: "SettingsId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SmtpConfigurations_SmtpSettingsId",
                table: "Users",
                column: "SmtpSettingsId",
                principalTable: "SmtpConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signature_Users_UserId",
                table: "Signature");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ImapConfigurations_ImapSettingsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SmtpConfigurations_SmtpSettingsId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SmtpSettingsId",
                table: "Employees",
                newName: "IX_Employees_SmtpSettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SettingsId",
                table: "Employees",
                newName: "IX_Employees_SettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ImapSettingsId",
                table: "Employees",
                newName: "IX_Employees_ImapSettingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ImapConfigurations_ImapSettingsId",
                table: "Employees",
                column: "ImapSettingsId",
                principalTable: "ImapConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Settings_SettingsId",
                table: "Employees",
                column: "SettingsId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SmtpConfigurations_SmtpSettingsId",
                table: "Employees",
                column: "SmtpSettingsId",
                principalTable: "SmtpConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Signature_Employees_UserId",
                table: "Signature",
                column: "UserId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
