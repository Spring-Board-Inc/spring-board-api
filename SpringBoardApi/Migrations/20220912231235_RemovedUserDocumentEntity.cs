using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class RemovedUserDocumentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDocuments");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDocuments_UserInformation_UserInformationId",
                        column: x => x.UserInformationId,
                        principalTable: "UserInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_UserInformationId",
                table: "UserDocuments",
                column: "UserInformationId");
        }
    }
}
