using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAdinux.Context.Migrations
{
    /// <inheritdoc />
    public partial class migaddarchivetoappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "appointments");
        }
    }
}
