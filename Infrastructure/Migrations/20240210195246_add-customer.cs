using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ClusterizationWorkspaces",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisibleType",
                table: "ClusterizationWorkspaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ClusterizationProfiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaces_OwnerId",
                table: "ClusterizationWorkspaces",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_OwnerId",
                table: "ClusterizationProfiles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationProfiles_Customers_OwnerId",
                table: "ClusterizationProfiles",
                column: "OwnerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationWorkspaces_Customers_OwnerId",
                table: "ClusterizationWorkspaces",
                column: "OwnerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationProfiles_Customers_OwnerId",
                table: "ClusterizationProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationWorkspaces_Customers_OwnerId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationWorkspaces_OwnerId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationProfiles_OwnerId",
                table: "ClusterizationProfiles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropColumn(
                name: "VisibleType",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ClusterizationProfiles");
        }
    }
}
