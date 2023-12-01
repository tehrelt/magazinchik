using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace maganzinchik.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cloth",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "sneaker_size",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ussize = table.Column<double>(name: "us_size", type: "double(3,1)", nullable: false),
                    eusize = table.Column<double>(name: "eu_size", type: "double(3,1)", nullable: false),
                    cmsize = table.Column<double>(name: "cm_size", type: "double(3,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "zip_type",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    manufacturerid = table.Column<ulong>(name: "manufacturer_id", type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "brand_ibfk_1",
                        column: x => x.manufacturerid,
                        principalTable: "manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sneaker",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    weight = table.Column<double>(type: "double(8,2)", nullable: false),
                    brandid = table.Column<ulong>(name: "brand_id", type: "bigint unsigned", nullable: false),
                    ziptypeid = table.Column<ulong>(name: "zip_type_id", type: "bigint unsigned", nullable: false),
                    clothid = table.Column<ulong>(name: "cloth_id", type: "bigint unsigned", nullable: false),
                    snsizetype = table.Column<ulong>(name: "sn_size_type", type: "bigint unsigned", nullable: false),
                    releasedate = table.Column<DateTime>(name: "release_date", type: "datetime", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "sneaker_ibfk_1",
                        column: x => x.brandid,
                        principalTable: "brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sneaker_ibfk_2",
                        column: x => x.clothid,
                        principalTable: "cloth",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sneaker_ibfk_3",
                        column: x => x.ziptypeid,
                        principalTable: "zip_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sneaker_ibfk_4",
                        column: x => x.snsizetype,
                        principalTable: "sneaker_size",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sneakers_photo",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    sneakerid = table.Column<ulong>(name: "sneaker_id", type: "bigint unsigned", nullable: false),
                    photoid = table.Column<ulong>(name: "photo_id", type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "sneakers_photo_ibfk_1",
                        column: x => x.sneakerid,
                        principalTable: "sneaker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sneakers_photo_ibfk_2",
                        column: x => x.photoid,
                        principalTable: "photo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "manufacturer_id",
                table: "brand",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "name",
                table: "brand",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "brand_id",
                table: "sneaker",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "cloth_id",
                table: "sneaker",
                column: "cloth_id");

            migrationBuilder.CreateIndex(
                name: "sn_size_type",
                table: "sneaker",
                column: "sn_size_type");

            migrationBuilder.CreateIndex(
                name: "zip_type_id",
                table: "sneaker",
                column: "zip_type_id");

            migrationBuilder.CreateIndex(
                name: "photo_id",
                table: "sneakers_photo",
                column: "photo_id");

            migrationBuilder.CreateIndex(
                name: "sneaker_id",
                table: "sneakers_photo",
                column: "sneaker_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sneakers_photo");

            migrationBuilder.DropTable(
                name: "sneaker");

            migrationBuilder.DropTable(
                name: "photo");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "cloth");

            migrationBuilder.DropTable(
                name: "zip_type");

            migrationBuilder.DropTable(
                name: "sneaker_size");

            migrationBuilder.DropTable(
                name: "manufacturer");
        }
    }
}
