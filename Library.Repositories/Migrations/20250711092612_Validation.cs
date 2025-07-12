using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Validation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventParticipants_LibraryEventId",
                table: "EventParticipants");

            migrationBuilder.RenameColumn(
                name: "SelectedRole",
                table: "AspNetUsers",
                newName: "UserRole");

            migrationBuilder.AlterColumn<string>(
                name: "UserCode",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CallingName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_LibraryEventId_ApplicationUserId",
                table: "EventParticipants",
                columns: new[] { "LibraryEventId", "ApplicationUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserCode",
                table: "AspNetUsers",
                column: "UserCode",
                unique: true,
                filter: "[UserCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventParticipants_LibraryEventId_ApplicationUserId",
                table: "EventParticipants");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserCode",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "AspNetUsers",
                newName: "SelectedRole");

            migrationBuilder.AlterColumn<string>(
                name: "UserCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CallingName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_LibraryEventId",
                table: "EventParticipants",
                column: "LibraryEventId");
        }
    }
}
