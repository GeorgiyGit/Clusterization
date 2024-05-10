using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataObjectNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExternalObjects_DataObjectId",
                table: "ExternalObjects");

            migrationBuilder.AlterColumn<long>(
                name: "DataObjectId",
                table: "ExternalObjects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalObjects_DataObjectId",
                table: "ExternalObjects",
                column: "DataObjectId",
                unique: true,
                filter: "[DataObjectId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExternalObjects_DataObjectId",
                table: "ExternalObjects");

            migrationBuilder.AlterColumn<long>(
                name: "DataObjectId",
                table: "ExternalObjects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExternalObjects_DataObjectId",
                table: "ExternalObjects",
                column: "DataObjectId",
                unique: true);
        }
    }
}
