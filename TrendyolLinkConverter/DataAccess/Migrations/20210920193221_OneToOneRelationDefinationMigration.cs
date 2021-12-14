using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class OneToOneRelationDefinationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2021, 9, 20, 21, 55, 31, 709, DateTimeKind.Local).AddTicks(302));

            migrationBuilder.AddColumn<int>(
                name: "request_id",
                schema: "public",
                table: "responses",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "requests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 22, 32, 20, 908, DateTimeKind.Local).AddTicks(4284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 20, 21, 55, 31, 703, DateTimeKind.Local).AddTicks(5325));

            migrationBuilder.CreateIndex(
                name: "IX_responses_request_id",
                schema: "public",
                table: "responses",
                column: "request_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_responses_requests_request_id",
                schema: "public",
                table: "responses",
                column: "request_id",
                principalSchema: "public",
                principalTable: "requests",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_responses_requests_request_id",
                schema: "public",
                table: "responses");

            migrationBuilder.DropIndex(
                name: "IX_responses_request_id",
                schema: "public",
                table: "responses");

            migrationBuilder.DropColumn(
                name: "request_id",
                schema: "public",
                table: "responses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "responses",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 21, 55, 31, 709, DateTimeKind.Local).AddTicks(302),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 20, 22, 32, 20, 915, DateTimeKind.Local).AddTicks(8420));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "requests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 21, 55, 31, 703, DateTimeKind.Local).AddTicks(5325),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 20, 22, 32, 20, 908, DateTimeKind.Local).AddTicks(4284));
        }
    }
}
