using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class FieldOfStudy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_FieldOfStudy_FieldOfStudyId",
                table: "LibraryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldOfStudy",
                table: "FieldOfStudy");

            migrationBuilder.RenameTable(
                name: "FieldOfStudy",
                newName: "FieldOfStudies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldOfStudies",
                table: "FieldOfStudies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_FieldOfStudies_FieldOfStudyId",
                table: "LibraryItems",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_FieldOfStudies_FieldOfStudyId",
                table: "LibraryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldOfStudies",
                table: "FieldOfStudies");

            migrationBuilder.RenameTable(
                name: "FieldOfStudies",
                newName: "FieldOfStudy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldOfStudy",
                table: "FieldOfStudy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_FieldOfStudy_FieldOfStudyId",
                table: "LibraryItems",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
