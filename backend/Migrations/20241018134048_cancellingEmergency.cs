using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class cancellingEmergency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmergencySchedules_EmergencyScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EmergencyRecords");

            migrationBuilder.DropTable(
                name: "EmergencySchedules");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmergencyScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmergencyScheduleId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmergencyScheduleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmergencySchedules",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencySchedules", x => x.ShiftId);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyRecords_EmergencySchedules_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "EmergencySchedules",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmergencyScheduleId",
                table: "AspNetUsers",
                column: "EmergencyScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyRecords_ShiftId",
                table: "EmergencyRecords",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmergencySchedules_EmergencyScheduleId",
                table: "AspNetUsers",
                column: "EmergencyScheduleId",
                principalTable: "EmergencySchedules",
                principalColumn: "ShiftId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
