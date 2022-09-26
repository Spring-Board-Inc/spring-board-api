using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class AddedDocTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6263af40-51cd-4728-8fa8-273b4d4167d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fad9ff5-cf97-4576-ad44-77e4417a863b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b32d9b61-16b3-4bd6-9cde-b9e8edb470d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5a479fa-2b8b-485f-a458-286ad21582ef");

            migrationBuilder.AddColumn<int>(
                name: "DocType",
                table: "UserDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DocType",
                table: "UserDocuments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6263af40-51cd-4728-8fa8-273b4d4167d4", "3dd18717-cdc8-4ca4-9101-84b084afdd53", "Administrator", "ADMINISTRATOR" },
                    { "9fad9ff5-cf97-4576-ad44-77e4417a863b", "60768fd5-14f9-4325-bc20-99cb18353287", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "b32d9b61-16b3-4bd6-9cde-b9e8edb470d3", "70595063-379b-4960-a686-3639454dcc7c", "Employer", "EMPLOYER" },
                    { "c5a479fa-2b8b-485f-a458-286ad21582ef", "b15575e1-334a-42ea-a232-71149598685f", "Applicant", "APPLICANT" }
                });
        }
    }
}
