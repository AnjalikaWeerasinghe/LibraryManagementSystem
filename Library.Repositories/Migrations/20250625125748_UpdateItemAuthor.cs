using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAuthors_LibraryItems_BookId",
                table: "ItemAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAuthors_LibraryItems_LibraryItemId",
                table: "ItemAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryEvents_LibraryInfos_LibraryInfoId",
                table: "LibraryEvents");

            migrationBuilder.DropIndex(
                name: "IX_ItemAuthors_LibraryItemId",
                table: "ItemAuthors");

            migrationBuilder.DropColumn(
                name: "LibraryItemId",
                table: "ItemAuthors");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryInfoId",
                table: "LibraryEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "ItemAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAuthors_LibraryItems_BookId",
                table: "ItemAuthors",
                column: "BookId",
                principalTable: "LibraryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryEvents_LibraryInfos_LibraryInfoId",
                table: "LibraryEvents",
                column: "LibraryInfoId",
                principalTable: "LibraryInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAuthors_LibraryItems_BookId",
                table: "ItemAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryEvents_LibraryInfos_LibraryInfoId",
                table: "LibraryEvents");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryInfoId",
                table: "LibraryEvents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "ItemAuthors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LibraryItemId",
                table: "ItemAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAuthors_LibraryItemId",
                table: "ItemAuthors",
                column: "LibraryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAuthors_LibraryItems_BookId",
                table: "ItemAuthors",
                column: "BookId",
                principalTable: "LibraryItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAuthors_LibraryItems_LibraryItemId",
                table: "ItemAuthors",
                column: "LibraryItemId",
                principalTable: "LibraryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryEvents_LibraryInfos_LibraryInfoId",
                table: "LibraryEvents",
                column: "LibraryInfoId",
                principalTable: "LibraryInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
