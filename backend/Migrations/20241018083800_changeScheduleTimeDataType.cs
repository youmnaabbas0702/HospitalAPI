using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeScheduleTimeDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "DoctorsSchedules",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "DoctorsSchedules",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "DoctorsSchedules",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "DoctorsSchedules",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}
