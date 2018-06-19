////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Data\Migrations\20180618100942_newfields18062018_3.cs
//
// summary:	Implements the 20180618100942 newfields 18062018 3 class
////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NNMetrics.Data.Migrations
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The newfields 18062018 3. </summary>
    ///
    /// <remarks>   Administrator, 19/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class newfields18062018_3 : Migration
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
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    teamName = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                });
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
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
