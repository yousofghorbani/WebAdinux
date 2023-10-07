using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAdinux.Context.Migrations
{
    /// <inheritdoc />
    public partial class mightmlcontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HtmlContent",
                table: "siteContent",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtmlContent",
                table: "siteContent");
        }
    }
}
