using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanThuongMaiDienTu.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeliveredToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "Order");
        }
    }
}
