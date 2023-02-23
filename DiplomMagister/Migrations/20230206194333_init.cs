using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomMagister.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotBefore = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EncodedJwt = table.Column<string>(type: "text", nullable: false),
                    Scope = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TokenId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Userinfo_Name = table.Column<string>(type: "text", nullable: false),
                    Userinfo_FirstName = table.Column<string>(type: "text", nullable: false),
                    Userinfo_Middlename = table.Column<string>(type: "text", nullable: true),
                    Userinfo_Lastname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClients_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    UserClientId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersData_UserClients_UserClientId",
                        column: x => x.UserClientId,
                        principalTable: "UserClients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClients_TokenId",
                table: "UserClients",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersData_UserClientId",
                table: "UsersData",
                column: "UserClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersData");

            migrationBuilder.DropTable(
                name: "UserClients");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
