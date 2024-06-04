using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlphaGym.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Description", "ImagePath", "ProductName", "UnitPrice" },
                values: new object[,]
                {
                    { 1, " Access to gym facilities. Entrance until 16:00 & 1 entry per day", "carconvert.png", "Basic Subscription", 30.0 },
                    { 2, " Access to all facilities and classes. No entry limit", "carearly.png", "Premium subcription", 50.0 },
                    { 3, " Access to all facilities and classes. No entry limit. Personalized workouts. Progress tracking and goal setting", "carfast.png", "Personal trainer subscription", 100.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
