using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Migrations
{
    public partial class constructors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Users_ImapConfigurations_ImapSettingsId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_Settings_SettingsId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_SmtpConfigurations_SmtpSettingsId",
                "Users");

            migrationBuilder.AlterColumn<int>(
                "SmtpSettingsId",
                "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                "SettingsId",
                "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Password",
                "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Login",
                "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                "ImapSettingsId",
                "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Address",
                "SmtpConfigurations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "SignatureText",
                "Signature",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Signature",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Address",
                "ImapConfigurations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Users_ImapConfigurations_ImapSettingsId",
                "Users",
                "ImapSettingsId",
                "ImapConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Users_Settings_SettingsId",
                "Users",
                "SettingsId",
                "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Users_SmtpConfigurations_SmtpSettingsId",
                "Users",
                "SmtpSettingsId",
                "SmtpConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Users_ImapConfigurations_ImapSettingsId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_Settings_SettingsId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_SmtpConfigurations_SmtpSettingsId",
                "Users");

            migrationBuilder.AlterColumn<int>(
                "SmtpSettingsId",
                "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                "SettingsId",
                "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                "Password",
                "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Login",
                "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                "ImapSettingsId",
                "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                "Address",
                "SmtpConfigurations",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "SignatureText",
                "Signature",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Signature",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                "Address",
                "ImapConfigurations",
                nullable: true,
                oldClrType: typeof(string));

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
    }
}