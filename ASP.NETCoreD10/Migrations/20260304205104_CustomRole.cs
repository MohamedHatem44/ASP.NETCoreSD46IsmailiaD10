using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NETCoreD10.Migrations
{
    /// <inheritdoc />
    public partial class CustomRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desciption",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desciption",
                table: "AspNetRoles");
        }
    }
}
