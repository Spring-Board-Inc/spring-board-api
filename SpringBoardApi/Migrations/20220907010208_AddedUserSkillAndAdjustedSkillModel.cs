using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class AddedUserSkillAndAdjustedSkillModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_UserInformation_UserInformationId",
                table: "Skills");

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

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Educations",
                newName: "Major");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserInformationId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    UserInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Skill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => new { x.UserInformationId, x.SkillId });
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_UserInformation_UserInformationId",
                table: "Skills",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_UserInformation_UserInformationId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "UserSkills");

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

            migrationBuilder.RenameColumn(
                name: "Major",
                table: "Educations",
                newName: "Course");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserInformationId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_UserInformation_UserInformationId",
                table: "Skills",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
