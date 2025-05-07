using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Quiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortAnswerQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortAnswerQuestion_Question_Id",
                        column: x => x.Id,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShortAnswerQuestion_CorrectAnswer",
                columns: table => new
                {
                    Text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortAnswerQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortAnswerQuestion_CorrectAnswer", x => new { x.ShortAnswerQuestionId, x.Text });
                    table.ForeignKey(
                        name: "FK_ShortAnswerQuestion_CorrectAnswer_ShortAnswerQuestion_ShortAnswerQuestionId",
                        column: x => x.ShortAnswerQuestionId,
                        principalTable: "ShortAnswerQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortAnswerQuestion_CorrectAnswer");

            migrationBuilder.DropTable(
                name: "ShortAnswerQuestion");
        }
    }
}
