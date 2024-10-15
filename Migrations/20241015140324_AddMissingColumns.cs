using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Specialities_SpecialityId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_SpecialityId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Admins");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Patients");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "SpecialityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "SpecialityId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "SpecialityId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 4,
                column: "SpecialityId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 5,
                column: "SpecialityId",
                value: 5);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_SpecialityId",
                table: "Admins",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Specialities_SpecialityId",
                table: "Admins",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
