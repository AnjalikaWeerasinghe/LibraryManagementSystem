using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLibraryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "LibraryItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "LibraryItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISSN",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Issue",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssueNumber",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedDate",
                table: "LibraryItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item Type",
                table: "LibraryItems",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "LibraryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Newspaper_ISSN",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Periodical_ISSN",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShelfLocation",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Volume",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "ItemAuthors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAuthors_BookId",
                table: "ItemAuthors",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAuthors_LibraryItems_BookId",
                table: "ItemAuthors",
                column: "BookId",
                principalTable: "LibraryItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAuthors_LibraryItems_BookId",
                table: "ItemAuthors");

            migrationBuilder.DropIndex(
                name: "IX_ItemAuthors_BookId",
                table: "ItemAuthors");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Field",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "ISSN",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Issue",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "IssueNumber",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "IssuedDate",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Item Type",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Newspaper_ISSN",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Periodical_ISSN",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "ShelfLocation",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "ItemAuthors");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "LibraryItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
