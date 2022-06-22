using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputerListing.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b3efc50c-9963-422a-b7de-634770bb6987", "e16e0096-8295-47c2-8bf6-1b80a5f36982", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8c92917-caaf-4df1-824c-073155f39143", "143cbd8e-f4cf-42e2-8a48-b3eaca9d401d", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3efc50c-9963-422a-b7de-634770bb6987");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8c92917-caaf-4df1-824c-073155f39143");
        }
    }
}
