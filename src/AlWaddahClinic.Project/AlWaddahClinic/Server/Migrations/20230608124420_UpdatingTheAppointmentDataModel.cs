using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlWaddahClinic.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTheAppointmentDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthRecordId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "190fa2cc-70bb-41f9-80fe-5d3131123422", "AQAAAAIAAYagAAAAENmpFjDMo3y4CQLl2pIoDQt976o24gn3exus/1S01+O7K7gwIR42/UAV1TbH3G21nw==" });
        }
    }
}
