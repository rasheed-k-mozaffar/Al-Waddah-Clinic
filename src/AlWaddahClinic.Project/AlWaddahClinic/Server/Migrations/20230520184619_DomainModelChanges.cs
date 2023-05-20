using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlWaddahClinic.Server.Migrations
{
    /// <inheritdoc />
    public partial class DomainModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_HealthRecords_HealthRecordId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_HealthRecordId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "HealthRecordId",
                table: "Medications");

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthRecordId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f950430b-b0ab-4247-a551-0079c7ae1de1", "AQAAAAIAAYagAAAAEH7l4R6hbmzEKd3C0kWWntHfOVF/3V8Pd8JdHMxTvjnZUNgMklucKsSJvr1Eq8sxbA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_HealthRecordId",
                table: "Notes",
                column: "HealthRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "HealthRecordId",
                table: "Medications",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0783b4ef-3051-4712-b7fd-f4baaa219f3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4974b9da-cd92-405b-aab6-3bfff1ba3240", "AQAAAAIAAYagAAAAECqX1Ss64zJ4XqNhjOxsWM4+CMCsF2Nt6Tc8jRYy9LPJWHuYvjofwMiPrZfX9aoDQg==" });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_HealthRecordId",
                table: "Medications",
                column: "HealthRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_HealthRecords_HealthRecordId",
                table: "Medications",
                column: "HealthRecordId",
                principalTable: "HealthRecords",
                principalColumn: "Id");
        }
    }
}
