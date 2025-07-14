using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldOfStudy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_Publishers_PublisherId",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Field",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "ShelfLocation",
                table: "LibraryItems");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "LibraryItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PublishedYear",
                table: "LibraryItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "FieldOfStudyId",
                table: "LibraryItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FieldOfStudy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOfStudy", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryItems_FieldOfStudyId",
                table: "LibraryItems",
                column: "FieldOfStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_FieldOfStudy_FieldOfStudyId",
                table: "LibraryItems",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_Publishers_PublisherId",
                table: "LibraryItems",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_FieldOfStudy_FieldOfStudyId",
                table: "LibraryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_Publishers_PublisherId",
                table: "LibraryItems");

            migrationBuilder.DropTable(
                name: "FieldOfStudy");

            migrationBuilder.DropIndex(
                name: "IX_LibraryItems_FieldOfStudyId",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "FieldOfStudyId",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "LibraryItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PublishedYear",
                table: "LibraryItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShelfLocation",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_Publishers_PublisherId",
                table: "LibraryItems",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
