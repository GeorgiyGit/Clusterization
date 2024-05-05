using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TelegramReplies_Date",
                table: "TelegramReplies",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_Date_TelegramRepliesCount_Views",
                table: "TelegramMessages",
                columns: new[] { "Date", "TelegramRepliesCount", "Views" });

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChannels_Date_TelegramMessagesCount_ParticipantsCount",
                table: "TelegramChannels",
                columns: new[] { "Date", "TelegramMessagesCount", "ParticipantsCount" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TelegramReplies_Date",
                table: "TelegramReplies");

            migrationBuilder.DropIndex(
                name: "IX_TelegramMessages_Date_TelegramRepliesCount_Views",
                table: "TelegramMessages");

            migrationBuilder.DropIndex(
                name: "IX_TelegramChannels_Date_TelegramMessagesCount_ParticipantsCount",
                table: "TelegramChannels");
        }
    }
}
