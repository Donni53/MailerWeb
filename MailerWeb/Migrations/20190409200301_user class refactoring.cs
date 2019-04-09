using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Migrations
{
    public partial class userclassrefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Users_SettingsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SettingsId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ConnectionSettingsId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "EncryptedPassword",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptedPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncryptedPassword_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncryptedPassword_UserId",
                table: "EncryptedPassword",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                table: "Users",
                column: "ConnectionSettingsId",
                principalTable: "ConnectionConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EncryptedPassword");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConnectionSettingsId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SettingsId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Localization = table.Column<string>(nullable: true),
                    Notifications = table.Column<bool>(nullable: false),
                    Theme = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SettingsId",
                table: "Users",
                column: "SettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                table: "Users",
                column: "ConnectionSettingsId",
                principalTable: "ConnectionConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users",
                column: "SettingsId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
