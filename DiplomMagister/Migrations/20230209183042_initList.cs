using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomMagister.Migrations
{
    public partial class initList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClients_Tokens_TokenId",
                table: "UserClients");

            migrationBuilder.DropIndex(
                name: "IX_UserClients_TokenId",
                table: "UserClients");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "UserClients");

            migrationBuilder.AddColumn<string>(
                name: "UserClientId",
                table: "Tokens",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserClientId",
                table: "Tokens",
                column: "UserClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_UserClients_UserClientId",
                table: "Tokens",
                column: "UserClientId",
                principalTable: "UserClients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_UserClients_UserClientId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserClientId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "UserClientId",
                table: "Tokens");

            migrationBuilder.AddColumn<int>(
                name: "TokenId",
                table: "UserClients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClients_TokenId",
                table: "UserClients",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClients_Tokens_TokenId",
                table: "UserClients",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id");
        }
    }
}
