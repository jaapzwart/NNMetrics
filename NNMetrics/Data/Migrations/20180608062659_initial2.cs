////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Data\Migrations\20180608062659_initial2.cs
//
// summary:	Implements the 20180608062659 initial 2 class
////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NNMetrics.Data.Migrations
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An initial 2. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class initial2 : Migration
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
            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletedForecast = table.Column<int>(nullable: false),
                    MTTR = table.Column<int>(nullable: false),
                    MeasureDate = table.Column<DateTime>(nullable: false),
                    NumberOfDeployments = table.Column<int>(nullable: false),
                    POSatisfaction = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.ID);
                });
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
            migrationBuilder.DropTable(
                name: "Metrics");
        }
    }
}
