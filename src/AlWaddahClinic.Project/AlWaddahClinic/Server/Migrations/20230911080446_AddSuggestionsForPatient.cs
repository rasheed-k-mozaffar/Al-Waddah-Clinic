using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlWaddahClinic.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSuggestionsForPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicalHistory",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suggestions",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalHistory",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Suggestions",
                table: "Patients");
        }
    }
}
