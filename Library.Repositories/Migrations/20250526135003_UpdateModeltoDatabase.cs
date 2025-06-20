using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModeltoDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryInfoId",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VendorId",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReservationCode",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MemberCode",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "LibraryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegisteredCode",
                table: "LibraryInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemCopyCode",
                table: "ItemCopies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FineCode",
                table: "Fines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DonorId",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LibraryInfoId",
                table: "Donors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedCode",
                table: "Borrowings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AcquisitionCode",
                table: "Acquisitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LibraryEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LibraryInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryEvents_LibraryInfos_LibraryInfoId",
                        column: x => x.LibraryInfoId,
                        principalTable: "LibraryInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    LibrarianId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_LibrarianId",
                        column: x => x.LibrarianId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryEventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipantStatus = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventParticipants_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventParticipants_LibraryEvents_LibraryEventId",
                        column: x => x.LibraryEventId,
                        principalTable: "LibraryEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_LibraryInfoId",
                table: "Vendors",
                column: "LibraryInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_LibraryInfoId",
                table: "Donors",
                column: "LibraryInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_ApplicationUserId",
                table: "EventParticipants",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_LibraryEventId",
                table: "EventParticipants",
                column: "LibraryEventId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryEvents_LibraryInfoId",
                table: "LibraryEvents",
                column: "LibraryInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LibrarianId",
                table: "Payments",
                column: "LibrarianId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PurchaseOrderId",
                table: "Payments",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_LibraryInfos_LibraryInfoId",
                table: "Donors",
                column: "LibraryInfoId",
                principalTable: "LibraryInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_LibraryInfos_LibraryInfoId",
                table: "Vendors",
                column: "LibraryInfoId",
                principalTable: "LibraryInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_LibraryInfos_LibraryInfoId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_LibraryInfos_LibraryInfoId",
                table: "Vendors");

            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "LibraryEvents");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_LibraryInfoId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Donors_LibraryInfoId",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "LibraryInfoId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ReservationCode",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "MemberCode",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "LibraryItems");

            migrationBuilder.DropColumn(
                name: "RegisteredCode",
                table: "LibraryInfos");

            migrationBuilder.DropColumn(
                name: "ItemCopyCode",
                table: "ItemCopies");

            migrationBuilder.DropColumn(
                name: "FineCode",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "LibraryInfoId",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "BorrowedCode",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "AcquisitionCode",
                table: "Acquisitions");
        }
    }
}
