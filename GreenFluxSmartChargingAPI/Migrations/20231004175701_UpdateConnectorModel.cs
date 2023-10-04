using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenFluxSmartChargingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConnectorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Identifier",
                table: "Connectors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Connectors");
        }
    }
}
