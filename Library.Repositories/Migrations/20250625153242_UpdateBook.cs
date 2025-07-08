using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_Genres_GenreId",
                table: "LibraryItems");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_Genres_GenreId",
                table: "LibraryItems",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_Genres_GenreId",
                table: "LibraryItems");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_Genres_GenreId",
                table: "LibraryItems",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
