using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Migrations
{
    public partial class connectionConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ImapConfigurations_ImapSettingsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SmtpConfigurations_SmtpSettingsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ImapSettingsId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmtpConfigurations",
                table: "SmtpConfigurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImapConfigurations",
                table: "ImapConfigurations");

            migrationBuilder.DropColumn(
                name: "ImapSettingsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Ssl",
                table: "SmtpConfigurations");

            migrationBuilder.DropColumn(
                name: "Ssl",
                table: "ImapConfigurations");

            migrationBuilder.RenameTable(
                name: "SmtpConfigurations",
                newName: "SmtpConfiguration");

            migrationBuilder.RenameTable(
                name: "ImapConfigurations",
                newName: "ImapConfiguration");

            migrationBuilder.RenameColumn(
                name: "SmtpSettingsId",
                table: "Users",
                newName: "ConnectionSettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SmtpSettingsId",
                table: "Users",
                newName: "IX_Users_ConnectionSettingsId");

            migrationBuilder.AlterColumn<int>(
                name: "SettingsId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmtpConfiguration",
                table: "SmtpConfiguration",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImapConfiguration",
                table: "ImapConfiguration",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ConnectionConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImapConfigurationId = table.Column<int>(nullable: false),
                    SmtpConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionConfigurations_ImapConfiguration_ImapConfigurationId",
                        column: x => x.ImapConfigurationId,
                        principalTable: "ImapConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionConfigurations_SmtpConfiguration_SmtpConfigurationId",
                        column: x => x.SmtpConfigurationId,
                        principalTable: "SmtpConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailDomain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Domain = table.Column<string>(nullable: false),
                    ConnectionConfigurationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDomain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailDomain_ConnectionConfigurations_ConnectionConfigurationId",
                        column: x => x.ConnectionConfigurationId,
                        principalTable: "ConnectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionConfigurations_ImapConfigurationId",
                table: "ConnectionConfigurations",
                column: "ImapConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionConfigurations_SmtpConfigurationId",
                table: "ConnectionConfigurations",
                column: "SmtpConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDomain_ConnectionConfigurationId",
                table: "EmailDomain",
                column: "ConnectionConfigurationId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EmailDomain");

            migrationBuilder.DropTable(
                name: "ConnectionConfigurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmtpConfiguration",
                table: "SmtpConfiguration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImapConfiguration",
                table: "ImapConfiguration");

            migrationBuilder.RenameTable(
                name: "SmtpConfiguration",
                newName: "SmtpConfigurations");

            migrationBuilder.RenameTable(
                name: "ImapConfiguration",
                newName: "ImapConfigurations");

            migrationBuilder.RenameColumn(
                name: "ConnectionSettingsId",
                table: "Users",
                newName: "SmtpSettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ConnectionSettingsId",
                table: "Users",
                newName: "IX_Users_SmtpSettingsId");

            migrationBuilder.AlterColumn<int>(
                name: "SettingsId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImapSettingsId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Ssl",
                table: "SmtpConfigurations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ssl",
                table: "ImapConfigurations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmtpConfigurations",
                table: "SmtpConfigurations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImapConfigurations",
                table: "ImapConfigurations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ImapSettingsId",
                table: "Users",
                column: "ImapSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ImapConfigurations_ImapSettingsId",
                table: "Users",
                column: "ImapSettingsId",
                principalTable: "ImapConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users",
                column: "SettingsId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SmtpConfigurations_SmtpSettingsId",
                table: "Users",
                column: "SmtpSettingsId",
                principalTable: "SmtpConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
