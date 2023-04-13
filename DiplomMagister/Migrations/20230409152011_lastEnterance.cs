using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomMagister.Migrations
{
    public partial class lastEnterance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ProfileInformation_LastEnterance",
                table: "UserClients",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileInformation_LastEnterance",
                table: "UserClients");
        }
    }
}
