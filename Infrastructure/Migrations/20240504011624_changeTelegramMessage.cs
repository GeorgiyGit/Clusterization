using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeTelegramMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flags",
                table: "TelegramMessages");

            migrationBuilder.DropColumn(
                name: "Forwards",
                table: "TelegramMessages");

            migrationBuilder.RenameColumn(
                name: "Flags2",
                table: "TelegramMessages",
                newName: "PostAuthor");

            migrationBuilder.AlterColumn<string>(
                name: "Emotion",
                table: "TelegramReactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "DocumentId",
                table: "TelegramReactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "TelegramReactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "TelegramID",
                table: "TelegramMessages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "TelegramReactions");

            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "TelegramReactions");

            migrationBuilder.RenameColumn(
                name: "PostAuthor",
                table: "TelegramMessages",
                newName: "Flags2");

            migrationBuilder.AlterColumn<string>(
                name: "Emotion",
                table: "TelegramReactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TelegramID",
                table: "TelegramMessages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Flags",
                table: "TelegramMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Forwards",
                table: "TelegramMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
