using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoardApi.Migrations
{
    public partial class AddeIsDeprecatedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "WorkExperiences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "UserInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "Skills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "JobTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "Industries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "Educations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "Certifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "Industries");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "Certifications");

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
    }
}
