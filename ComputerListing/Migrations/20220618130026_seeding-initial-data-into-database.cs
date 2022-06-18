using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputerListing.Migrations
{
    public partial class seedinginitialdataintodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Computers",
                columns: new[] { "Id", "Manufacturer", "Model", "Proccessor", "RAM" },
                values: new object[] { 1, "MSI", "Crosshair 15", "i7-11800", 16 });

            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "Id", "ComputerId", "Name" },
                values: new object[] { 1, 1, "Gaming Mouse" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
