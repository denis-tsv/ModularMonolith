using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Order.DataAccess.MsSql.Migrations
{
    public partial class Order_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Order");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Order",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "Order",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                schema: "Order",
                table: "OrderItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Order");
        }
    }
}
