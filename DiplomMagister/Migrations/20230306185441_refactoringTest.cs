using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomMagister.Migrations
{
    public partial class refactoringTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAbs_Tests_TestId",
                table: "QuestionAbs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAbs",
                table: "QuestionAbs");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAbs_TestId",
                table: "QuestionAbs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "QuestionAbs");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "QuestionAbs");

            migrationBuilder.RenameTable(
                name: "QuestionAbs",
                newName: "BasicQuestions");

            migrationBuilder.AddColumn<List<string>>(
                name: "Questions",
                table: "Tests",
                type: "text[]",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IndexOfTrue",
                table: "BasicQuestions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string[]>(
                name: "Answers",
                table: "BasicQuestions",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0],
                oldClrType: typeof(string[]),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasicQuestions",
                table: "BasicQuestions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BasicQuestions",
                table: "BasicQuestions");

            migrationBuilder.DropColumn(
                name: "Questions",
                table: "Tests");

            migrationBuilder.RenameTable(
                name: "BasicQuestions",
                newName: "QuestionAbs");

            migrationBuilder.AlterColumn<int>(
                name: "IndexOfTrue",
                table: "QuestionAbs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string[]>(
                name: "Answers",
                table: "QuestionAbs",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "QuestionAbs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "QuestionAbs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAbs",
                table: "QuestionAbs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAbs_TestId",
                table: "QuestionAbs",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAbs_Tests_TestId",
                table: "QuestionAbs",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
