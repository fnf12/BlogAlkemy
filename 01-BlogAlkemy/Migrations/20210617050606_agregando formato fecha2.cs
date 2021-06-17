using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_BlogAlkemy.Migrations
{
    public partial class agregandoformatofecha2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "creationDate",
                table: "Post",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "CONVERT(VARCHAR(10), GETDATE(), 105)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "creationDate",
                table: "Post",
                type: "date",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR(10), GETDATE(), 105)",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
