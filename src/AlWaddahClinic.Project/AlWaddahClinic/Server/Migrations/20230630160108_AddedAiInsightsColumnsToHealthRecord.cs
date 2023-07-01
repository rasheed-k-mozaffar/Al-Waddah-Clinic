using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlWaddahClinic.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedAiInsightsColumnsToHealthRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicalCaseInsight",
                table: "HealthRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientSuggestion",
                table: "HealthRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedMedicalCases",
                table: "HealthRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuggestedMedicalTests",
                table: "HealthRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalCaseInsight",
                table: "HealthRecords");

            migrationBuilder.DropColumn(
                name: "PatientSuggestion",
                table: "HealthRecords");

            migrationBuilder.DropColumn(
                name: "RelatedMedicalCases",
                table: "HealthRecords");

            migrationBuilder.DropColumn(
                name: "SuggestedMedicalTests",
                table: "HealthRecords");
        }
    }
}
