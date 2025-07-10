using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUsertoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AspNetUsers_ApplicatioUserId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ApplicatioUserId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_EventParticipants_ApplicationUserId",
                table: "EventParticipants");

            migrationBuilder.DropIndex(
                name: "IX_DivisionStaff_ApplicationUserId",
                table: "DivisionStaff");

            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "DivisionStaff");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicatioUserId",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Members",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedRole",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_ApplicationUserId",
                table: "Members",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_ApplicationUserId",
                table: "EventParticipants",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionStaff_ApplicationUserId",
                table: "DivisionStaff",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AspNetUsers_ApplicationUserId",
                table: "Members",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AspNetUsers_ApplicationUserId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ApplicationUserId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_EventParticipants_ApplicationUserId",
                table: "EventParticipants");

            migrationBuilder.DropIndex(
                name: "IX_DivisionStaff_ApplicationUserId",
                table: "DivisionStaff");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SelectedRole",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicatioUserId",
                table: "Members",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "DivisionStaff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Members_ApplicatioUserId",
                table: "Members",
                column: "ApplicatioUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_ApplicationUserId",
                table: "EventParticipants",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionStaff_ApplicationUserId",
                table: "DivisionStaff",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AspNetUsers_ApplicatioUserId",
                table: "Members",
                column: "ApplicatioUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
