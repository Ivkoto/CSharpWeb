using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Migrations
{
    public partial class UsersSharedAlbums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSharedAlbums",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SharedAlbumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSharedAlbums", x => new { x.UserId, x.SharedAlbumId });
                    table.ForeignKey(
                        name: "FK_UserSharedAlbums_Albums_SharedAlbumId",
                        column: x => x.SharedAlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSharedAlbums_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSharedAlbums_SharedAlbumId",
                table: "UserSharedAlbums",
                column: "SharedAlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSharedAlbums");
        }
    }
}
