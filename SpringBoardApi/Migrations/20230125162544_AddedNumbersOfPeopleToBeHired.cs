using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class AddedNumbersOfPeopleToBeHired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27aec839-5fa7-4dab-8295-c5fb64dd0c64");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44e6e525-9fc7-46e7-99e3-9ce309c664ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f2f0ddf-3d09-4ec2-93ab-e2f7af6db872");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f769fa5-0753-4772-82c3-39dd1dc5759b");

            migrationBuilder.AddColumn<int>(
                name: "NumbersToBeHired",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44e6e525-9fc7-46e7-99e3-9ce309c664ff", "677e6ef6-a7eb-4143-91b2-37a858ae1f16", "Administrator", "ADMINISTRATOR" },
                    { "27aec839-5fa7-4dab-8295-c5fb64dd0c64", "0a756968-67bd-4e6e-9863-bd823cf36619", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "8f2f0ddf-3d09-4ec2-93ab-e2f7af6db872", "53969479-15ea-4be7-9e63-bdb02ff195d7", "Applicant", "APPLICANT" },
                    { "8f769fa5-0753-4772-82c3-39dd1dc5759b", "553cdc39-d6c6-4b7a-90c2-0cee1a1834f8", "Employer", "EMPLOYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "NumbersToBeHired",
                table: "Jobs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27aec839-5fa7-4dab-8295-c5fb64dd0c64", "0a756968-67bd-4e6e-9863-bd823cf36619", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "44e6e525-9fc7-46e7-99e3-9ce309c664ff", "677e6ef6-a7eb-4143-91b2-37a858ae1f16", "Administrator", "ADMINISTRATOR" },
                    { "8f2f0ddf-3d09-4ec2-93ab-e2f7af6db872", "53969479-15ea-4be7-9e63-bdb02ff195d7", "Applicant", "APPLICANT" },
                    { "8f769fa5-0753-4772-82c3-39dd1dc5759b", "553cdc39-d6c6-4b7a-90c2-0cee1a1834f8", "Employer", "EMPLOYER" }
                });
        }
    }
}
