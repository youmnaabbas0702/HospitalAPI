using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatingDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_DoctorId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsSchedules_AspNetUsers_DoctorId1",
                table: "DoctorsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencySchedules_AspNetUsers_DoctorId1",
                table: "EmergencySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_AspNetUsers_PatientId1",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_DoctorId1",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_PatientId1",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_DoctorId1",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_PatientId1",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistories_PatientId1",
                table: "MedicalHistories");

            migrationBuilder.DropIndex(
                name: "IX_EmergencySchedules_DoctorId1",
                table: "EmergencySchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsSchedules_DoctorId1",
                table: "DoctorsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "EmergencySchedules");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "DoctorsSchedules");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "MedicalRecords",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "MedicalRecords",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "MedicalHistories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "EmergencySchedules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "DoctorsSchedules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId",
                table: "MedicalRecords",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencySchedules_DoctorId",
                table: "EmergencySchedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsSchedules_DoctorId",
                table: "DoctorsSchedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsSchedules_AspNetUsers_DoctorId",
                table: "DoctorsSchedules",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencySchedules_AspNetUsers_DoctorId",
                table: "EmergencySchedules",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_AspNetUsers_PatientId",
                table: "MedicalHistories",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_DoctorId",
                table: "MedicalRecords",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_PatientId",
                table: "MedicalRecords",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsSchedules_AspNetUsers_DoctorId",
                table: "DoctorsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencySchedules_AspNetUsers_DoctorId",
                table: "EmergencySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_AspNetUsers_PatientId",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_DoctorId",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_PatientId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_DoctorId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories");

            migrationBuilder.DropIndex(
                name: "IX_EmergencySchedules_DoctorId",
                table: "EmergencySchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsSchedules_DoctorId",
                table: "DoctorsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId1",
                table: "MedicalRecords",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientId1",
                table: "MedicalRecords",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "MedicalHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PatientId1",
                table: "MedicalHistories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "EmergencySchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId1",
                table: "EmergencySchedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "DoctorsSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId1",
                table: "DoctorsSchedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId1",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientId1",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId1",
                table: "MedicalRecords",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId1",
                table: "MedicalRecords",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PatientId1",
                table: "MedicalHistories",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencySchedules_DoctorId1",
                table: "EmergencySchedules",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsSchedules_DoctorId1",
                table: "DoctorsSchedules",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId1",
                table: "Appointments",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId1",
                table: "Appointments",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_DoctorId1",
                table: "Appointments",
                column: "DoctorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId1",
                table: "Appointments",
                column: "PatientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsSchedules_AspNetUsers_DoctorId1",
                table: "DoctorsSchedules",
                column: "DoctorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencySchedules_AspNetUsers_DoctorId1",
                table: "EmergencySchedules",
                column: "DoctorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_AspNetUsers_PatientId1",
                table: "MedicalHistories",
                column: "PatientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_DoctorId1",
                table: "MedicalRecords",
                column: "DoctorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_PatientId1",
                table: "MedicalRecords",
                column: "PatientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
