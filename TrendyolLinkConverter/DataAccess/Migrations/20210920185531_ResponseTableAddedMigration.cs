using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class ResponseTableAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "requests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 21, 55, 31, 703, DateTimeKind.Local).AddTicks(5325),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 20, 21, 46, 35, 858, DateTimeKind.Local).AddTicks(9128));

            migrationBuilder.CreateTable(
                name: "responses",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2021, 9, 20, 21, 55, 31, 709, DateTimeKind.Local).AddTicks(302)),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    content = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_responses", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "responses",
                schema: "public");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                schema: "public",
                table: "requests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 21, 46, 35, 858, DateTimeKind.Local).AddTicks(9128),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 20, 21, 55, 31, 703, DateTimeKind.Local).AddTicks(5325));
        }
    }
}
