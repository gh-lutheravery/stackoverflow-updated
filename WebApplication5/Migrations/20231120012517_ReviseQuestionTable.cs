using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    /// <inheritdoc />
    public partial class ReviseQuestionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcceptedAnswerId",
                table: "Question",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnswerCount",
                table: "Question",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedAnswerId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "AnswerCount",
                table: "Question");
        }
    }
}
