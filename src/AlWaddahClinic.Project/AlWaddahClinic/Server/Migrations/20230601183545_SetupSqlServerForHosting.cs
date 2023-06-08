using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlWaddahClinic.Server.Migrations
{
    /// <inheritdoc />
    public partial class SetupSqlServerForHosting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartAt",
                table: "Appointments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "190fa2cc-70bb-41f9-80fe-5d3131123422", "AQAAAAIAAYagAAAAENmpFjDMo3y4CQLl2pIoDQt976o24gn3exus/1S01+O7K7gwIR42/UAV1TbH3G21nw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartAt",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f950430b-b0ab-4247-a551-0079c7ae1de1", "AQAAAAIAAYagAAAAEH7l4R6hbmzEKd3C0kWWntHfOVF/3V8Pd8JdHMxTvjnZUNgMklucKsSJvr1Eq8sxbA==" });
        }
    }
}
