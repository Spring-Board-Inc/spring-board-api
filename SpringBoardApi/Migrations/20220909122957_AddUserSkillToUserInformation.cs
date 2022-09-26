using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class AddUserSkillToUserInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_UserInformation_UserInformationId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_UserInformationId",
                table: "Skills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eded1f0-69e8-404c-9223-f07063077225");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "340bc13d-1aa8-4ca9-b7f7-9427ecc1e075");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c3cb3fa-619f-4fbb-9c92-b322fbc3907a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff5d24b3-b719-4017-bb15-de8c75c795bf");

            migrationBuilder.DropColumn(
                name: "UserInformationId",
                table: "Skills");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36009f6a-35be-4707-aad2-a909fe461a68", "c60ea3e0-7293-4fb8-96b9-2b22637e213c", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "719e09b3-ae27-4c84-8d04-83a0b9bb501a", "ae41b7bb-4bed-48ec-ab4f-16264af8f852", "Applicant", "APPLICANT" },
                    { "a1ce21b2-60ca-43f6-9d1e-4abaff5021f4", "cc94f28e-28d3-4f47-9cf7-b4ae32cade66", "Employer", "EMPLOYER" },
                    { "dc679dfc-9a0f-40e7-8601-f18d77f07f02", "57d0b10a-a096-4bc1-b205-8f0f6941e951", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_UserInformation_UserInformationId",
                table: "UserSkills",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_UserInformation_UserInformationId",
                table: "UserSkills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36009f6a-35be-4707-aad2-a909fe461a68");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "719e09b3-ae27-4c84-8d04-83a0b9bb501a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1ce21b2-60ca-43f6-9d1e-4abaff5021f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc679dfc-9a0f-40e7-8601-f18d77f07f02");

            migrationBuilder.AddColumn<Guid>(
                name: "UserInformationId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1eded1f0-69e8-404c-9223-f07063077225", "d68b32ff-643d-45a3-ab24-bbadae91a0d1", "Administrator", "ADMINISTRATOR" },
                    { "340bc13d-1aa8-4ca9-b7f7-9427ecc1e075", "90c722df-f66e-45c2-9272-2fcdfd6699d7", "Employer", "EMPLOYER" },
                    { "7c3cb3fa-619f-4fbb-9c92-b322fbc3907a", "b557eb67-6aa0-4916-8843-e29eb4f225be", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "ff5d24b3-b719-4017-bb15-de8c75c795bf", "027328f9-33b8-4817-b90c-0efe3ea1d4d0", "Applicant", "APPLICANT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UserInformationId",
                table: "Skills",
                column: "UserInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_UserInformation_UserInformationId",
                table: "Skills",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id");
        }
    }
}
