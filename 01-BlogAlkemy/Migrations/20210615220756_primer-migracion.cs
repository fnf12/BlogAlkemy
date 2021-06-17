using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_BlogAlkemy.Migrations
{
    public partial class primermigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    idCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.idCategory);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    idPost = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCategory = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: false),
                    image = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    creationDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.idPost);
                    table.ForeignKey(
                        name: "FK_post_category",
                        column: x => x.idCategory,
                        principalTable: "category",
                        principalColumn: "idCategory",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_idCategory",
                table: "Post",
                column: "idCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
