using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class AddedExtraColumnsToJobsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23ef86ca-ee48-4594-9931-da4fbc966cd9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68d011bb-630b-42e1-92cf-140a434b5a0a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71ac84ef-959c-4110-b564-e4acf0ed0733");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78b91174-8b7b-41ed-b2f2-86ce2fe50d44");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosingDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "SalaryLowerRange",
                table: "Jobs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalaryUpperRange",
                table: "Jobs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60892ead-0f54-4bd6-a95d-1466475ccd02", "f4c07323-18a6-4493-80bb-5d749b040b94", "Administrator", "ADMINISTRATOR" },
                    { "691ac63e-1695-4777-9c92-fb889a898957", "bc71c069-cd7b-433a-96cf-a539791d83fa", "Applicant", "APPLICANT" },
                    { "a52e5157-1819-4f8d-a8a5-25565ad27bf3", "6886f02f-5fea-4c75-8f67-b03feb9dfa29", "Employer", "EMPLOYER" },
                    { "b932012b-3604-43f9-b521-06f2b4ad91a9", "5e49905d-732b-4646-95d7-6eada1d952dc", "SuperAdministrator", "SUPERADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60892ead-0f54-4bd6-a95d-1466475ccd02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "691ac63e-1695-4777-9c92-fb889a898957");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a52e5157-1819-4f8d-a8a5-25565ad27bf3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b932012b-3604-43f9-b521-06f2b4ad91a9");

            migrationBuilder.DropColumn(
                name: "ClosingDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "SalaryLowerRange",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "SalaryUpperRange",
                table: "Jobs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23ef86ca-ee48-4594-9931-da4fbc966cd9", "186224a8-19db-4bf9-bd90-1b4661a6a1fd", "Applicant", "APPLICANT" },
                    { "68d011bb-630b-42e1-92cf-140a434b5a0a", "bc9306ba-06b5-414d-97a7-a834c73f470a", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "71ac84ef-959c-4110-b564-e4acf0ed0733", "eb8f6302-14a6-4ab0-8409-dfa00e837cfe", "Employer", "EMPLOYER" },
                    { "78b91174-8b7b-41ed-b2f2-86ce2fe50d44", "1335984f-8011-420a-906b-902fa560e1cd", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
