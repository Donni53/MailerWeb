using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Migrations
{
    public partial class connectionConfig : Migration
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

            migrationBuilder.DropIndex(
                "IX_Users_ImapSettingsId",
                "Users");

            migrationBuilder.DropPrimaryKey(
                "PK_SmtpConfigurations",
                "SmtpConfigurations");

            migrationBuilder.DropPrimaryKey(
                "PK_ImapConfigurations",
                "ImapConfigurations");

            migrationBuilder.DropColumn(
                "ImapSettingsId",
                "Users");

            migrationBuilder.DropColumn(
                "Ssl",
                "SmtpConfigurations");

            migrationBuilder.DropColumn(
                "Ssl",
                "ImapConfigurations");

            migrationBuilder.RenameTable(
                "SmtpConfigurations",
                newName: "SmtpConfiguration");

            migrationBuilder.RenameTable(
                "ImapConfigurations",
                newName: "ImapConfiguration");

            migrationBuilder.RenameColumn(
                "SmtpSettingsId",
                "Users",
                "ConnectionSettingsId");

            migrationBuilder.RenameIndex(
                "IX_Users_SmtpSettingsId",
                table: "Users",
                newName: "IX_Users_ConnectionSettingsId");

            migrationBuilder.AlterColumn<int>(
                "SettingsId",
                "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                "PK_SmtpConfiguration",
                "SmtpConfiguration",
                "Id");

            migrationBuilder.AddPrimaryKey(
                "PK_ImapConfiguration",
                "ImapConfiguration",
                "Id");

            migrationBuilder.CreateTable(
                "ConnectionConfigurations",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    ImapConfigurationId = table.Column<int>(nullable: false),
                    SmtpConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionConfigurations", x => x.Id);
                    table.ForeignKey(
                        "FK_ConnectionConfigurations_ImapConfiguration_ImapConfigurationId",
                        x => x.ImapConfigurationId,
                        "ImapConfiguration",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ConnectionConfigurations_SmtpConfiguration_SmtpConfigurationId",
                        x => x.SmtpConfigurationId,
                        "SmtpConfiguration",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "EmailDomain",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Domain = table.Column<string>(nullable: false),
                    ConnectionConfigurationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDomain", x => x.Id);
                    table.ForeignKey(
                        "FK_EmailDomain_ConnectionConfigurations_ConnectionConfigurationId",
                        x => x.ConnectionConfigurationId,
                        "ConnectionConfigurations",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_ConnectionConfigurations_ImapConfigurationId",
                "ConnectionConfigurations",
                "ImapConfigurationId");

            migrationBuilder.CreateIndex(
                "IX_ConnectionConfigurations_SmtpConfigurationId",
                "ConnectionConfigurations",
                "SmtpConfigurationId");

            migrationBuilder.CreateIndex(
                "IX_EmailDomain_ConnectionConfigurationId",
                "EmailDomain",
                "ConnectionConfigurationId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                "Users");

            migrationBuilder.DropForeignKey(
                "FK_Users_Settings_SettingsId",
                "Users");

            migrationBuilder.DropTable(
                "EmailDomain");

            migrationBuilder.DropTable(
                "ConnectionConfigurations");

            migrationBuilder.DropPrimaryKey(
                "PK_SmtpConfiguration",
                "SmtpConfiguration");

            migrationBuilder.DropPrimaryKey(
                "PK_ImapConfiguration",
                "ImapConfiguration");

            migrationBuilder.RenameTable(
                "SmtpConfiguration",
                newName: "SmtpConfigurations");

            migrationBuilder.RenameTable(
                "ImapConfiguration",
                newName: "ImapConfigurations");

            migrationBuilder.RenameColumn(
                "ConnectionSettingsId",
                "Users",
                "SmtpSettingsId");

            migrationBuilder.RenameIndex(
                "IX_Users_ConnectionSettingsId",
                table: "Users",
                newName: "IX_Users_SmtpSettingsId");

            migrationBuilder.AlterColumn<int>(
                "SettingsId",
                "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                "ImapSettingsId",
                "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                "Ssl",
                "SmtpConfigurations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "Ssl",
                "ImapConfigurations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                "PK_SmtpConfigurations",
                "SmtpConfigurations",
                "Id");

            migrationBuilder.AddPrimaryKey(
                "PK_ImapConfigurations",
                "ImapConfigurations",
                "Id");

            migrationBuilder.CreateIndex(
                "IX_Users_ImapSettingsId",
                "Users",
                "ImapSettingsId");

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
    }
}