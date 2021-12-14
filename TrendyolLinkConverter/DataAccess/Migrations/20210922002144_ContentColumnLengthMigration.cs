using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ContentColumnLengthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "responses",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 22, 3, 21, 44, 83, DateTimeKind.Local).AddTicks(540),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 20, 22, 32, 20, 915, DateTimeKind.Local).AddTicks(8420));

            migrationBuilder.AlterColumn<string>(
                name: "content",
                schema: "public",
                table: "responses",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "requests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 22, 3, 21, 44, 75, DateTimeKind.Local).AddTicks(3950),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 20, 22, 32, 20, 908, DateTimeKind.Local).AddTicks(4284));

            migrationBuilder.AlterColumn<string>(
                name: "content",
                schema: "public",
                table: "requests",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "responses",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 22, 32, 20, 915, DateTimeKind.Local).AddTicks(8420),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 22, 3, 21, 44, 83, DateTimeKind.Local).AddTicks(540));

            migrationBuilder.AlterColumn<string>(
                name: "content",
                schema: "public",
                table: "responses",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "requests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 22, 32, 20, 908, DateTimeKind.Local).AddTicks(4284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 22, 3, 21, 44, 75, DateTimeKind.Local).AddTicks(3950));

            migrationBuilder.AlterColumn<string>(
                name: "content",
                schema: "public",
                table: "requests",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000);
        }
    }
}
