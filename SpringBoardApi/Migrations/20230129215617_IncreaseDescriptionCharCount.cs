using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class IncreaseDescriptionCharCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6642164f-2be1-43a1-9416-8fb4f49c29c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66faad25-eb4b-4cc7-adbc-858f59a8d6d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "763df99a-1397-45ae-912e-ed6084bc4a5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a17f157a-33ab-442c-97f2-c7d41920bcfe");

            migrationBuilder.AlterColumn<string>(
                name: "Descriptions",
                table: "WorkExperiences",
                type: "nvarchar(1500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d31bf5d-150f-4b7d-b7ad-5a3bf3f2cb06", "15884afc-1b64-4a34-91d7-0824637fe806", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "51c70757-a138-4cb9-8503-365b3e1d3fa3", "13b216d6-9075-442e-a4fd-eb80274409f2", "Applicant", "APPLICANT" },
                    { "aadf47ca-bcd9-42ed-b1f6-ab45d8fa862e", "344c1da3-7cfa-4825-82ba-b5e5e2881076", "Administrator", "ADMINISTRATOR" },
                    { "fe92eed3-a117-4e8e-a3a3-fee4b168084a", "75f9cb87-8f2e-471a-b312-e06530f14ba9", "Employer", "EMPLOYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d31bf5d-150f-4b7d-b7ad-5a3bf3f2cb06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51c70757-a138-4cb9-8503-365b3e1d3fa3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aadf47ca-bcd9-42ed-b1f6-ab45d8fa862e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe92eed3-a117-4e8e-a3a3-fee4b168084a");

            migrationBuilder.AlterColumn<string>(
                name: "Descriptions",
                table: "WorkExperiences",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6642164f-2be1-43a1-9416-8fb4f49c29c5", "94a81310-20c1-4f25-999a-445da81c7754", "Administrator", "ADMINISTRATOR" },
                    { "66faad25-eb4b-4cc7-adbc-858f59a8d6d8", "57ae50a3-3ade-4529-80d6-28ebd61079f1", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "763df99a-1397-45ae-912e-ed6084bc4a5d", "87814f85-5a80-43f5-bce8-5da37d2ebf1b", "Applicant", "APPLICANT" },
                    { "a17f157a-33ab-442c-97f2-c7d41920bcfe", "6db9cafc-dc8e-495f-9b56-a66c1b142f27", "Employer", "EMPLOYER" }
                });
        }
    }
}
