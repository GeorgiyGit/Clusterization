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
                name: "IX_Videos_PublishedAtDateTimeOffset_CommentCount_ViewCount",
                table: "Videos",
                columns: new[] { "PublishedAtDateTimeOffset", "CommentCount", "ViewCount" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PublishedAtDateTimeOffset",
                table: "Comments",
                column: "PublishedAtDateTimeOffset");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_PublishedAtDateTimeOffset_VideoCount_SubscriberCount",
                table: "Channels",
                columns: new[] { "PublishedAtDateTimeOffset", "VideoCount", "SubscriberCount" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Videos_PublishedAtDateTimeOffset_CommentCount_ViewCount",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PublishedAtDateTimeOffset",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Channels_PublishedAtDateTimeOffset_VideoCount_SubscriberCount",
                table: "Channels");
        }
    }
}
