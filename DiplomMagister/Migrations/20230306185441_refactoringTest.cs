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
                table: "IQuestionAbs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAbs",
                table: "IQuestionAbs");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAbs_TestId",
                table: "IQuestionAbs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "IQuestionAbs");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "IQuestionAbs");

            migrationBuilder.RenameTable(
                name: "IQuestionAbs",
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
                newName: "IQuestionAbs");

            migrationBuilder.AlterColumn<int>(
                name: "IndexOfTrue",
                table: "IQuestionAbs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string[]>(
                name: "Answers",
                table: "IQuestionAbs",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "IQuestionAbs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "IQuestionAbs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAbs",
                table: "IQuestionAbs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAbs_TestId",
                table: "IQuestionAbs",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAbs_Tests_TestId",
                table: "IQuestionAbs",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
