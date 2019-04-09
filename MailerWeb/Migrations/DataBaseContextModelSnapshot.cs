﻿// <auto-generated />

using MailerWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MailerWeb.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    internal class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MailerWeb.Models.ConnectionConfiguration", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ImapConfigurationId");

                b.Property<int>("SmtpConfigurationId");

                b.HasKey("Id");

                b.HasIndex("ImapConfigurationId");

                b.HasIndex("SmtpConfigurationId");

                b.ToTable("ConnectionConfigurations");
            });

            modelBuilder.Entity("MailerWeb.Models.EmailDomain", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int?>("ConnectionConfigurationId");

                b.Property<string>("Domain")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("ConnectionConfigurationId");

                b.ToTable("EmailDomain");
            });

            modelBuilder.Entity("MailerWeb.Models.EncryptedPassword", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Password")
                    .IsRequired();

                b.Property<int?>("UserId");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("EncryptedPassword");
            });

            modelBuilder.Entity("MailerWeb.Models.ImapConfiguration", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Address")
                    .IsRequired();

                b.Property<int>("Port");

                b.HasKey("Id");

                b.ToTable("ImapConfiguration");
            });

            modelBuilder.Entity("MailerWeb.Models.Signature", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("SignatureText")
                    .IsRequired()
                    .HasMaxLength(1000);

                b.Property<int?>("UserId");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("Signature");
            });

            modelBuilder.Entity("MailerWeb.Models.SmtpConfiguration", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Address")
                    .IsRequired();

                b.Property<int>("Port");

                b.HasKey("Id");

                b.ToTable("SmtpConfiguration");
            });

            modelBuilder.Entity("MailerWeb.Models.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int?>("ConnectionSettingsId");

                b.Property<string>("Login");

                b.Property<string>("Name");

                b.Property<string>("Password");

                b.HasKey("Id");

                b.HasIndex("ConnectionSettingsId");

                b.ToTable("Users");
            });

            modelBuilder.Entity("MailerWeb.Models.ConnectionConfiguration", b =>
            {
                b.HasOne("MailerWeb.Models.ImapConfiguration", "ImapConfiguration")
                    .WithMany()
                    .HasForeignKey("ImapConfigurationId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MailerWeb.Models.SmtpConfiguration", "SmtpConfiguration")
                    .WithMany()
                    .HasForeignKey("SmtpConfigurationId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MailerWeb.Models.EmailDomain", b =>
            {
                b.HasOne("MailerWeb.Models.ConnectionConfiguration")
                    .WithMany("DomainsList")
                    .HasForeignKey("ConnectionConfigurationId");
            });

            modelBuilder.Entity("MailerWeb.Models.EncryptedPassword", b =>
            {
                b.HasOne("MailerWeb.Models.User")
                    .WithMany("EncryptedPasswords")
                    .HasForeignKey("UserId");
            });

            modelBuilder.Entity("MailerWeb.Models.Signature", b =>
            {
                b.HasOne("MailerWeb.Models.User")
                    .WithMany("Signatures")
                    .HasForeignKey("UserId");
            });

            modelBuilder.Entity("MailerWeb.Models.User", b =>
            {
                b.HasOne("MailerWeb.Models.ConnectionConfiguration", "ConnectionSettings")
                    .WithMany()
                    .HasForeignKey("ConnectionSettingsId");
            });
#pragma warning restore 612, 618
        }
    }
}