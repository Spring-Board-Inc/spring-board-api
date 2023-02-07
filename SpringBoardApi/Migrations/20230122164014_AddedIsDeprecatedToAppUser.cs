using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class AddedIsDeprecatedToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c6c6c3b-82a2-4517-b7a5-287cca475e30");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "556d4a19-1de5-45b2-923a-0944085e78ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad4e50f3-266d-409e-8041-07c64141b8b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e96cbf8d-0c27-4f55-9859-0a919d0e6674");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c6c6c3b-82a2-4517-b7a5-287cca475e30", "f22e1794-2b5b-4042-acd4-fd86c92d76b6", "Employer", "EMPLOYER" },
                    { "556d4a19-1de5-45b2-923a-0944085e78ab", "acf2fdf7-c544-4212-8700-68fdd8c60b91", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "ad4e50f3-266d-409e-8041-07c64141b8b0", "8c8fb9c4-60f7-450e-9145-641205203872", "Applicant", "APPLICANT" },
                    { "e96cbf8d-0c27-4f55-9859-0a919d0e6674", "fcb957bd-0f92-487c-a01f-b1864c799b4e", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
