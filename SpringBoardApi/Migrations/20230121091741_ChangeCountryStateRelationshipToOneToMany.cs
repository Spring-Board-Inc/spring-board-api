using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class ChangeCountryStateRelationshipToOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_States_CountryId",
                table: "States");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bd4f058-a088-4897-a4e3-6e8aa3779507");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab6db76e-56aa-4399-8141-709e3d86880f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b39cc20b-acec-4156-bc84-16794f37cb24");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d786fbc6-ee6e-4a52-a03f-17adf334b704");

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

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_States_CountryId",
                table: "States");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2bd4f058-a088-4897-a4e3-6e8aa3779507", "0333cb37-8183-469a-a4ab-5c420bf72bd9", "Administrator", "ADMINISTRATOR" },
                    { "ab6db76e-56aa-4399-8141-709e3d86880f", "d71edaed-323b-471a-a73a-f3654f310eed", "Applicant", "APPLICANT" },
                    { "b39cc20b-acec-4156-bc84-16794f37cb24", "25b82eea-3215-4d99-b96d-53624ec2df64", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "d786fbc6-ee6e-4a52-a03f-17adf334b704", "c32944c1-ae94-4e10-971e-8c7bc1fe3afa", "Employer", "EMPLOYER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId",
                unique: true);
        }
    }
}
