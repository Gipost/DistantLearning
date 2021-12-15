using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DistantLearning.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_questions_tests_TestId",
                        column: x => x.TestId,
                        principalTable: "tests",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_answersCompleted_QuestionID",
                table: "answersCompleted",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_answersCompleted_TestCompleteID",
                table: "answersCompleted",
                column: "TestCompleteID");

            migrationBuilder.CreateIndex(
                name: "IX_questions_TestId",
                table: "questions",
                column: "TestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_groupID",
                table: "Students",
                column: "groupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
