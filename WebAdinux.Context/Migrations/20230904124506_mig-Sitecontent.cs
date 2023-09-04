using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAdinux.Context.Migrations
{
    /// <inheritdoc />
    public partial class migSitecontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "siteContent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    HeaderId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModiFiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_siteContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_siteContent_siteHeaders_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "siteHeaders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_siteContent_HeaderId",
                table: "siteContent",
                column: "HeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "siteContent");
        }
    }
}
