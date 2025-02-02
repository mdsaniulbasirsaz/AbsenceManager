using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbsenceManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolesWithStaticGuids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33a621d2-faa4-408b-9bd5-8dfa7da69f33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0e3166a-ccea-401e-830d-0d1908a479b3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75574b51-546e-485e-8340-3583a9977601", null, "Admin", "ADMIN" },
                    { "f3837fe1-793b-420b-8186-8165b7e97599", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75574b51-546e-485e-8340-3583a9977601");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3837fe1-793b-420b-8186-8165b7e97599");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33a621d2-faa4-408b-9bd5-8dfa7da69f33", null, "User", "USER" },
                    { "e0e3166a-ccea-401e-830d-0d1908a479b3", null, "Admin", "ADMIN" }
                });
        }
    }
}
