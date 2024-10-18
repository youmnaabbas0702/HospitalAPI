using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class relationOfEmergency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmergencySchedules_AspNetUsers_DoctorId",
                table: "EmergencySchedules");

            migrationBuilder.DropIndex(
                name: "IX_EmergencySchedules_DoctorId",
                table: "EmergencySchedules");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "EmergencySchedules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "EmergencyScheduleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmergencyScheduleId",
                table: "AspNetUsers",
                column: "EmergencyScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmergencySchedules_EmergencyScheduleId",
                table: "AspNetUsers",
                column: "EmergencyScheduleId",
                principalTable: "EmergencySchedules",
                principalColumn: "ShiftId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmergencySchedules_EmergencyScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmergencyScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmergencyScheduleId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "EmergencySchedules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencySchedules_DoctorId",
                table: "EmergencySchedules",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencySchedules_AspNetUsers_DoctorId",
                table: "EmergencySchedules",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
