using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomMagister.Migrations
{
    public partial class setNullableToToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClients_Tokens_TokenId",
                table: "UserClients");

            migrationBuilder.AlterColumn<int>(
                name: "TokenId",
                table: "UserClients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClients_Tokens_TokenId",
                table: "UserClients",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClients_Tokens_TokenId",
                table: "UserClients");

            migrationBuilder.AlterColumn<int>(
                name: "TokenId",
                table: "UserClients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClients_Tokens_TokenId",
                table: "UserClients",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
