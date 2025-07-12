using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Publishedmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*─────────────────────────────────────────────
      1. LibraryItems – rename YearPublished → PublishedYear
    ─────────────────────────────────────────────*/

            // 1‑A Add new column first (nullable false but with default)
            migrationBuilder.AddColumn<int>(
                name: "PublishedYear",
                table: "LibraryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);


            // 1‑C Drop the old column
            migrationBuilder.DropColumn(
                name: "YearPublished",
                table: "LibraryItems");

            /*─────────────────────────────────────────────
              2. AspNetUserTokens – shrink LoginProvider & Name
            ─────────────────────────────────────────────*/

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            /*─────────────────────────────────────────────
              3. AspNetUserLogins – shrink LoginProvider & ProviderKey
            ─────────────────────────────────────────────*/

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey", "UserId" });

            /*─────────────────────────────────────────────
              4. New table: UserCodeCounters
            ─────────────────────────────────────────────*/

            migrationBuilder.CreateTable(
                name: "UserCodeCounters",
                columns: table => new
                {
                    RoleKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastNumber = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCodeCounters", x => x.RoleKey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCodeCounters");

            migrationBuilder.DropColumn(
                name: "PublishedYear",
                table: "LibraryItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "YearPublished",
                table: "LibraryItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }
    }
}
