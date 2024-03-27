using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changetasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "MyTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MyTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_CustomerId",
                table: "MyTasks",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyTasks_AspNetUsers_CustomerId",
                table: "MyTasks",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyTasks_AspNetUsers_CustomerId",
                table: "MyTasks");

            migrationBuilder.DropIndex(
                name: "IX_MyTasks_CustomerId",
                table: "MyTasks");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "MyTasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MyTasks");
        }
    }
}
