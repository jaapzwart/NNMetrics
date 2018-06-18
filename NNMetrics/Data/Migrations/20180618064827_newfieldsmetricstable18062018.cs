using Microsoft.EntityFrameworkCore.Migrations;

namespace NNMetrics.Data.Migrations
{
    public partial class newfieldsmetricstable18062018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductOwner",
                table: "Metrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScrumMaster",
                table: "Metrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "Metrics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOwner",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "ScrumMaster",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "Metrics");
        }
    }
}