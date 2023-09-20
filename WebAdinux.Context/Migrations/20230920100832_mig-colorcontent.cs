using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAdinux.Context.Migrations
{
    /// <inheritdoc />
    public partial class migcolorcontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "siteContent",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "siteContent");
        }
    }
}
