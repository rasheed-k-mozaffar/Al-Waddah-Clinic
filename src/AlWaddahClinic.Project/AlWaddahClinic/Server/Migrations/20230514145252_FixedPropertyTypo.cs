using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlWaddahClinic.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixedPropertyTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmaillAddress",
                table: "Patients",
                newName: "EmailAddress");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4974b9da-cd92-405b-aab6-3bfff1ba3240", "AQAAAAIAAYagAAAAECqX1Ss64zJ4XqNhjOxsWM4+CMCsF2Nt6Tc8jRYy9LPJWHuYvjofwMiPrZfX9aoDQg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Patients",
                newName: "EmaillAddress");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fa62fb24-fbfe-4c0d-9924-92d1a4e600f4", "AQAAAAIAAYagAAAAEAXcG/RTIs2KVUwGefXhXK6ubQMrCIVzbNQIl+AYAKQUv6DZ9oNOkve1CysaYH68lA==" });
        }
    }
}
