using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_BlogAlkemy.Migrations
{
    public partial class agregandoformatofecha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Post",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(MAX)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "creationDate",
                table: "Post",
                type: "date",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR(10), GETDATE(), 105)",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "GETUTCDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "image",
                table: "Post",
                type: "varbinary(MAX)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "creationDate",
                table: "Post",
                type: "date",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "CONVERT(VARCHAR(10), GETDATE(), 105)");
        }
    }
}
