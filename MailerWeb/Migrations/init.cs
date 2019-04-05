using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ImapConfigurations",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Ssl = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_ImapConfigurations", x => x.Id); });

            migrationBuilder.CreateTable(
                "Settings",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Theme = table.Column<string>(nullable: true),
                    Localization = table.Column<string>(nullable: true),
                    Notifications = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Settings", x => x.Id); });

            migrationBuilder.CreateTable(
                "SmtpConfigurations",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Ssl = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_SmtpConfigurations", x => x.Id); });

            migrationBuilder.CreateTable(
                "Employees",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    ImapSettingsId = table.Column<int>(nullable: true),
                    SmtpSettingsId = table.Column<int>(nullable: true),
                    SettingsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        "FK_Employees_ImapConfigurations_ImapSettingsId",
                        x => x.ImapSettingsId,
                        "ImapConfigurations",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Employees_Settings_SettingsId",
                        x => x.SettingsId,
                        "Settings",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Employees_SmtpConfigurations_SmtpSettingsId",
                        x => x.SmtpSettingsId,
                        "SmtpConfigurations",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Signature",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SignatureText = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signature", x => x.Id);
                    table.ForeignKey(
                        "FK_Signature_Employees_UserId",
                        x => x.UserId,
                        "Employees",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Employees_ImapSettingsId",
                "Employees",
                "ImapSettingsId");

            migrationBuilder.CreateIndex(
                "IX_Employees_SettingsId",
                "Employees",
                "SettingsId");

            migrationBuilder.CreateIndex(
                "IX_Employees_SmtpSettingsId",
                "Employees",
                "SmtpSettingsId");

            migrationBuilder.CreateIndex(
                "IX_Signature_UserId",
                "Signature",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Signature");

            migrationBuilder.DropTable(
                "Employees");

            migrationBuilder.DropTable(
                "ImapConfigurations");

            migrationBuilder.DropTable(
                "Settings");

            migrationBuilder.DropTable(
                "SmtpConfigurations");
        }
    }
}