using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class MakeSomeJobColumnsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "075fbb0f-0391-4501-b8d0-25bfdbc39d97");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3de68781-21ee-44d8-9453-947bb730271c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62672b08-fcc9-403a-8b11-d64f9fd1d04a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99fecdf5-6db6-4306-bb4e-735c9c9fc62c");

            migrationBuilder.AlterColumn<double>(
                name: "SalaryUpperRange",
                table: "Jobs",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "SalaryLowerRange",
                table: "Jobs",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosingDate",
                table: "Jobs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a9bb274-879c-4452-94d8-e4b60871abfe", "56056af6-6ba3-4cf2-8248-3d14daddd74f", "Administrator", "ADMINISTRATOR" },
                    { "900472d2-25bc-4ddb-ac05-d6da77cef52b", "78e15d71-2d76-4e9d-b60a-b37ac84f46a1", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "9aaf137a-49ff-4298-b5a9-5b6849ac72c2", "2163f2bf-5ed8-44ae-b23a-c9ab46efbaa2", "Employer", "EMPLOYER" },
                    { "ce08f414-d196-49cf-b21f-75f4117f5f3c", "19570e3d-d270-4684-9581-bc92ec573ffc", "Applicant", "APPLICANT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a9bb274-879c-4452-94d8-e4b60871abfe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "900472d2-25bc-4ddb-ac05-d6da77cef52b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aaf137a-49ff-4298-b5a9-5b6849ac72c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce08f414-d196-49cf-b21f-75f4117f5f3c");

            migrationBuilder.AlterColumn<double>(
                name: "SalaryUpperRange",
                table: "Jobs",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "SalaryLowerRange",
                table: "Jobs",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosingDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "075fbb0f-0391-4501-b8d0-25bfdbc39d97", "cf25167f-568b-4d9a-95f0-79a33589f55a", "Employer", "EMPLOYER" },
                    { "3de68781-21ee-44d8-9453-947bb730271c", "d96dd60e-9e75-421a-8dfe-439b41d4cfd2", "Applicant", "APPLICANT" },
                    { "62672b08-fcc9-403a-8b11-d64f9fd1d04a", "fdbc1b9b-6d90-4225-8353-e09802b2d97d", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "99fecdf5-6db6-4306-bb4e-735c9c9fc62c", "2b21150c-3a22-44df-980a-e049d258ebd9", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
