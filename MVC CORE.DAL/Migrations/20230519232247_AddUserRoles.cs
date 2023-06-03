using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_CORE.DDL.Migrations
{
    public partial class AddUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8bda0e38-1e85-49be-a39c-2e047d772374", "b9145fbf-2561-459d-9ec3-4935b2ce93c3", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "feb96376-9232-4430-a437-e3cdc170799e", "69f57275-e6e0-4ba3-9576-5786d3355ac1", "User", "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bda0e38-1e85-49be-a39c-2e047d772374");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "feb96376-9232-4430-a437-e3cdc170799e");
        }
    }
}
