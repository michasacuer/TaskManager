using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Persistence.Migrations
{
    public partial class Deleted_Nullable_pole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Projects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "EndedTasks",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Projects",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "EndedTasks",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
