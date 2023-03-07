using System;
using System.Collections.Generic;
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
                name: "TestInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mark = table.Column<double>(type: "double precision", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProfileInformation_Name = table.Column<string>(type: "text", nullable: false),
                    ProfileInformation_FirstName = table.Column<string>(type: "text", nullable: false),
                    ProfileInformation_Lastname = table.Column<string>(type: "text", nullable: false),
                    ProfileInformation_Mail = table.Column<string>(type: "text", nullable: false),
                    ProfileInformation_EditingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProfileInformation_Privacy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OwnerId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileSettings_UserClients_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserClients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserClientId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_UserClients_UserClientId",
                        column: x => x.UserClientId,
                        principalTable: "UserClients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatorId = table.Column<string>(type: "text", nullable: false),
                    TestInfoId = table.Column<int>(type: "integer", nullable: false),
                    Visibility = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_TestInfo_TestInfoId",
                        column: x => x.TestInfoId,
                        principalTable: "TestInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tests_UserClients_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    UserClientId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagDTO_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagDTO_UserClients_UserClientId",
                        column: x => x.UserClientId,
                        principalTable: "UserClients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionAbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Essence = table.Column<string>(type: "text", nullable: false),
                    QuestionType = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TotalScore = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: true),
                    Answers = table.Column<List<string>>(type: "text[]", nullable: true),
                    IndexOfTrue = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAbs_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    UserClientId = table.Column<string>(type: "text", nullable: true),
                    Persent = table.Column<int>(type: "integer", nullable: false),
                    Mark = table.Column<int>(type: "integer", nullable: false),
                    Completed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistics_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_UserClients_UserClientId",
                        column: x => x.UserClientId,
                        principalTable: "UserClients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagTest",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "integer", nullable: false),
                    TestsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTest", x => new { x.TagsId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_TagTest_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTest_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileSettings_OwnerId",
                table: "ProfileSettings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAbs_TestId",
                table: "QuestionAbs",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_TestId",
                table: "Statistics",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserClientId",
                table: "Statistics",
                column: "UserClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDTO_TagId",
                table: "TagDTO",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDTO_UserClientId",
                table: "TagDTO",
                column: "UserClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserClientId",
                table: "Tags",
                column: "UserClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTest_TestsId",
                table: "TagTest",
                column: "TestsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_CreatorId",
                table: "Tests",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestInfoId",
                table: "Tests",
                column: "TestInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileSettings");

            migrationBuilder.DropTable(
                name: "QuestionAbs");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "TagDTO");

            migrationBuilder.DropTable(
                name: "TagTest");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "TestInfo");

            migrationBuilder.DropTable(
                name: "UserClients");
        }
    }
}
