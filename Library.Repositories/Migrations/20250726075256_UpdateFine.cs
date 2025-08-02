using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_FineTypes_FineTypeId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "FineCode",
                table: "Fines");

            migrationBuilder.AlterColumn<int>(
                name: "FineTypeId",
                table: "Fines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Fines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_FineTypes_FineTypeId",
                table: "Fines",
                column: "FineTypeId",
                principalTable: "FineTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_FineTypes_FineTypeId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Fines");

            migrationBuilder.AlterColumn<int>(
                name: "FineTypeId",
                table: "Fines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FineCode",
                table: "Fines",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_FineTypes_FineTypeId",
                table: "Fines",
                column: "FineTypeId",
                principalTable: "FineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
