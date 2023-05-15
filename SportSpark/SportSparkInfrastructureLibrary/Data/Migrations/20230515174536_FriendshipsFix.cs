using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSparkInfrastructureLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class FriendshipsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_User2Id",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_UserId",
                table: "Friendship");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Friendship",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "Friendship",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_UserId",
                table: "Friendship",
                newName: "IX_Friendship_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_User2Id",
                table: "Friendship",
                newName: "IX_Friendship_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_ReceiverId",
                table: "Friendship",
                column: "ReceiverId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_SenderId",
                table: "Friendship",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_ReceiverId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_SenderId",
                table: "Friendship");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Friendship",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Friendship",
                newName: "User2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_SenderId",
                table: "Friendship",
                newName: "IX_Friendship_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_ReceiverId",
                table: "Friendship",
                newName: "IX_Friendship_User2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_User2Id",
                table: "Friendship",
                column: "User2Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_UserId",
                table: "Friendship",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
