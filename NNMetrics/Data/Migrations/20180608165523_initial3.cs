////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Data\Migrations\20180608165523_initial3.cs
//
// summary:	Implements the 20180608165523 initial 3 class
////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore.Migrations;

namespace NNMetrics.Data.Migrations
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An initial 3. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class initial3 : Migration
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Ups the given migration builder. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="migrationBuilder"> The migration builder. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "Metrics",
                nullable: true);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Downs the given migration builder. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="migrationBuilder"> The migration builder. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userName",
                table: "Metrics");
        }
    }
}