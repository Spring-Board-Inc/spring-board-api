using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class ChangedEnumsToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61a744f4-73ca-4899-86d5-55990cbfd8da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad0a3a3d-6975-4800-a5b7-cd4216de2559");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7baff04-e446-4852-9ed5-3362dfc1f43c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd590b97-658b-48e6-a67b-639268b23113");

            migrationBuilder.AlterColumn<string>(
                name: "DocType",
                table: "UserDocuments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LevelOfEducation",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01fd4538-f99d-495e-a102-36818dc94b78", "ed2b92d1-a16d-493e-84d7-00d7b41b294e", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "2b08f340-69df-4cde-872d-5a992a72f036", "5c08580d-df7b-4001-968c-45deb68fafa4", "Administrator", "ADMINISTRATOR" },
                    { "92070629-d289-4d54-8568-48bc0fe1bc7b", "ad973275-ad23-4eaf-b9d7-c071cec13ff9", "Employer", "EMPLOYER" },
                    { "f2672278-f282-4b67-9c1d-8c551d772f99", "7e689438-0f4e-4094-9ca1-d72d60bd0d41", "Applicant", "APPLICANT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01fd4538-f99d-495e-a102-36818dc94b78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b08f340-69df-4cde-872d-5a992a72f036");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92070629-d289-4d54-8568-48bc0fe1bc7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2672278-f282-4b67-9c1d-8c551d772f99");

            migrationBuilder.AlterColumn<int>(
                name: "DocType",
                table: "UserDocuments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Skills",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LevelOfEducation",
                table: "Educations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61a744f4-73ca-4899-86d5-55990cbfd8da", "45a82e71-fd7a-43dc-9d8e-eb802c0a214f", "Applicant", "APPLICANT" },
                    { "ad0a3a3d-6975-4800-a5b7-cd4216de2559", "a3d1d371-8746-4786-a9fa-ac13265b7fff", "Employer", "EMPLOYER" },
                    { "d7baff04-e446-4852-9ed5-3362dfc1f43c", "3104162c-1953-434c-9f72-c27caaa507fc", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "fd590b97-658b-48e6-a67b-639268b23113", "a5920965-07b6-419f-ac0f-ea50fb676cf5", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
