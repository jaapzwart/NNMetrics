////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Data\Migrations\20180618064827_newfieldsmetricstable18062018.cs
//
// summary:	Implements the 20180618064827 newfieldsmetricstable 18062018 class
////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore.Migrations;

namespace NNMetrics.Data.Migrations
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A newfieldsmetricstable 18062018. </summary>
    ///
    /// <remarks>   Administrator, 19/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class newfieldsmetricstable18062018 : Migration
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Ups the given migration builder. </summary>
        ///
        /// <remarks>   Administrator, 19/06/2018. </remarks>
        ///
        /// <param name="migrationBuilder"> The migration builder. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Downs the given migration builder. </summary>
        ///
        /// <remarks>   Administrator, 19/06/2018. </remarks>
        ///
        /// <param name="migrationBuilder"> The migration builder. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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