using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbsenceManager.Migrations
{
    /// <inheritdoc />
    public partial class FixNonDeterministicModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20c88b57-f3f8-44d1-a339-4c17d282a6ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ae7cef5-83e0-43a8-b4bc-3a0677ae0248");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33a621d2-faa4-408b-9bd5-8dfa7da69f33", null, "User", "USER" },
                    { "e0e3166a-ccea-401e-830d-0d1908a479b3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "20c88b57-f3f8-44d1-a339-4c17d282a6ed", null, "Admin", "ADMIN" },
                    { "7ae7cef5-83e0-43a8-b4bc-3a0677ae0248", null, "User", "USER" }
                });
        }
    }
}
