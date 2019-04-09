using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Migrations
{
    public partial class userclassrefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_Settings_SettingsId",
                "Users");

            migrationBuilder.DropTable(
                "Settings");

            migrationBuilder.DropIndex(
                "IX_Users_SettingsId",
                "Users");

            migrationBuilder.DropColumn(
                "Nickname",
                "Users");

            migrationBuilder.DropColumn(
                "SettingsId",
                "Users");

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
                "ConnectionSettingsId",
                "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                "EncryptedPassword",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptedPassword", x => x.Id);
                    table.ForeignKey(
                        "FK_EncryptedPassword_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_EncryptedPassword_UserId",
                "EncryptedPassword",
                "UserId");

            migrationBuilder.AddForeignKey(
                "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                "Users",
                "ConnectionSettingsId",
                "ConnectionConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                "Users");

            migrationBuilder.DropTable(
                "EncryptedPassword");

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
                "ConnectionSettingsId",
                "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                "Nickname",
                "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "SettingsId",
                "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                "Settings",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Localization = table.Column<string>(nullable: true),
                    Notifications = table.Column<bool>(nullable: false),
                    Theme = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Settings", x => x.Id); });

            migrationBuilder.CreateIndex(
                "IX_Users_SettingsId",
                "Users",
                "SettingsId");

            migrationBuilder.AddForeignKey(
                "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                "Users",
                "ConnectionSettingsId",
                "ConnectionConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Users_Settings_SettingsId",
                "Users",
                "SettingsId",
                "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}