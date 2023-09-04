using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAdinux.Context.Migrations
{
    /// <inheritdoc />
    public partial class migSiteHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "siteHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasDropDown = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModiFiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_siteHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_siteHeaders_siteHeaders_ParentId",
                        column: x => x.ParentId,
                        principalTable: "siteHeaders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_siteHeaders_ParentId",
                table: "siteHeaders",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "siteHeaders");
        }
    }
}
