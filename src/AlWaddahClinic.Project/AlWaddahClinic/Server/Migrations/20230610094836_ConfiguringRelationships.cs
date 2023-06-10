using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlWaddahClinic.Server.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguringRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_HealthRecords_HealthRecordId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_HealthRecordId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "HealthRecordId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "HealthRecords",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c3beeca4-c876-4475-8bb7-a1bec2457ceb", "AQAAAAIAAYagAAAAENCKivU6wy+Ua0tJlczt+l1XG0hV54FO/xgdlkoHCh13WFJTibl4WDhErYJVLRRn0w==" });

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_AppointmentId",
                table: "HealthRecords",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Appointments_AppointmentId",
                table: "HealthRecords",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Appointments_AppointmentId",
                table: "HealthRecords");

            migrationBuilder.DropIndex(
                name: "IX_HealthRecords_AppointmentId",
                table: "HealthRecords");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "HealthRecords");

            migrationBuilder.AddColumn<int>(
                name: "HealthRecordId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4c85b841-5e77-4c3d-ba94-ea2cbc806ce0", "AQAAAAIAAYagAAAAEODtYe6oGjD0gpu0b8LklNzgFwFiD/y7lVqdbFMYcjkoPHpNegvsrAR6UIcH9cAWUw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_HealthRecordId",
                table: "Appointments",
                column: "HealthRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_HealthRecords_HealthRecordId",
                table: "Appointments",
                column: "HealthRecordId",
                principalTable: "HealthRecords",
                principalColumn: "Id");
        }
    }
}
