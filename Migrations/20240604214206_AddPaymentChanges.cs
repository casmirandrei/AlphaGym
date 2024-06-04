using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaGym.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PurchasedItem",
                columns: table => new
                {
                    PurchasedItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedItem", x => x.PurchasedItemID);
                    table.ForeignKey(
                        name: "FK_PurchasedItem_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedItem_PaymentID",
                table: "PurchasedItem",
                column: "PaymentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchasedItem");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Payments");
        }
    }
}
