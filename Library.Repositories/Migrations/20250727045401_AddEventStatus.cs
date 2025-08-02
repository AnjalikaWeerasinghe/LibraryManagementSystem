using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddEventStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventStatus",
                table: "LibraryEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventStatus",
                table: "LibraryEvents");
        }
    }
}
