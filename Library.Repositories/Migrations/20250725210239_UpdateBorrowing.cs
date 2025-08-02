using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBorrowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Members_MemberId",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "ItemCopyCode",
                table: "ItemCopies");

            migrationBuilder.DropColumn(
                name: "ItemStatus",
                table: "ItemCopies");

            migrationBuilder.DropColumn(
                name: "MemeberId",
                table: "Borrowings");

            migrationBuilder.RenameColumn(
                name: "BorrowedCode",
                table: "Borrowings",
                newName: "UserId");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "ItemCopies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Borrowings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Borrowings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_ApplicationUserId",
                table: "Borrowings",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_AspNetUsers_ApplicationUserId",
                table: "Borrowings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Members_MemberId",
                table: "Borrowings",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_AspNetUsers_ApplicationUserId",
                table: "Borrowings");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Members_MemberId",
                table: "Borrowings");

            migrationBuilder.DropIndex(
                name: "IX_Borrowings_ApplicationUserId",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "Available",
                table: "ItemCopies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Borrowings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Borrowings",
                newName: "BorrowedCode");

            migrationBuilder.AddColumn<string>(
                name: "ItemCopyCode",
                table: "ItemCopies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ItemStatus",
                table: "ItemCopies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Borrowings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemeberId",
                table: "Borrowings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Members_MemberId",
                table: "Borrowings",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
