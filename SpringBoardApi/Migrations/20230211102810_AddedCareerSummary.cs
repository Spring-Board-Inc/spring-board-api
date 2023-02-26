using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class AddedCareerSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CareerSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerSummaries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CareerSummaries_UserId",
                table: "CareerSummaries",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareerSummaries");

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
    }
}
