using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailerWeb.Server.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImapConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    Port = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImapConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmtpConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    Port = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpConfiguration", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ConnectionSettingsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_ConnectionConfigurations_ConnectionSettingsId",
                        column: x => x.ConnectionSettingsId,
                        principalTable: "ConnectionConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Signature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    SignatureText = table.Column<string>(maxLength: 1000, nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signature_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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

            migrationBuilder.CreateIndex(
                name: "IX_EncryptedPassword_UserId",
                table: "EncryptedPassword",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Signature_UserId",
                table: "Signature",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ConnectionSettingsId",
                table: "Users",
                column: "ConnectionSettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailDomain");

            migrationBuilder.DropTable(
                name: "EncryptedPassword");

            migrationBuilder.DropTable(
                name: "Signature");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ConnectionConfigurations");

            migrationBuilder.DropTable(
                name: "ImapConfiguration");

            migrationBuilder.DropTable(
                name: "SmtpConfiguration");
        }
    }
}
