using Microsoft.EntityFrameworkCore.Migrations;

namespace Famous.Quote.Quiz.Infrastructure.Migrations
{
    public partial class AddSortIndexColumnInAnswersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "Answers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "Answers");
        }
    }
}
