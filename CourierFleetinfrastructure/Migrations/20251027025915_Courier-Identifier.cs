using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierFleetInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourierIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Couriers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Couriers");
        }
    }
}
