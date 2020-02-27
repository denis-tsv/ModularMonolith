using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Order.DataAccess.MsSql.Migrations
{
    public partial class AddProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Order",
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 1, "First", 10m });

            migrationBuilder.InsertData(
                schema: "Order",
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 2, "Second", 20m });

            migrationBuilder.InsertData(
                schema: "Order",
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 3, "Third", 10m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Order",
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Order",
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Order",
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
