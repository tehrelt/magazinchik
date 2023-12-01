using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace maganzinchik.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MovePhotoUrlToSneakersPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "sneakers_photo_ibfk_2",
                table: "sneakers_photo");

            migrationBuilder.DropTable(
                name: "photo");

            migrationBuilder.DropIndex(
                name: "photo_id",
                table: "sneakers_photo");

            migrationBuilder.DropColumn(
                name: "photo_id",
                table: "sneakers_photo");

            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                table: "sneakers_photo",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_url",
                table: "sneakers_photo");

            migrationBuilder.AddColumn<ulong>(
                name: "photo_id",
                table: "sneakers_photo",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.CreateTable(
                name: "photo",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    url = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "photo_id",
                table: "sneakers_photo",
                column: "photo_id");

            migrationBuilder.AddForeignKey(
                name: "sneakers_photo_ibfk_2",
                table: "sneakers_photo",
                column: "photo_id",
                principalTable: "photo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
