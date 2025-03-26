using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_Children_and_Adults_to_string : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultsCount",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ChildrenCount",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "AdultsMenu",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChildrenMenu",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultsMenu",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ChildrenMenu",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "AdultsCount",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildrenCount",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
