using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlWaddahClinic.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderPropertyToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fa62fb24-fbfe-4c0d-9924-92d1a4e600f4", "AQAAAAIAAYagAAAAEAXcG/RTIs2KVUwGefXhXK6ubQMrCIVzbNQIl+AYAKQUv6DZ9oNOkve1CysaYH68lA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ff1781a-ed02-4a1b-8578-de5cf255b0e4", "AQAAAAIAAYagAAAAEBYHyrXeNDdl0HAE/7wndcz43Lbrhb+Xfx3VrS+twu+yC0NwyD/b4EeT6pvUHCSY6Q==" });
        }
    }
}
