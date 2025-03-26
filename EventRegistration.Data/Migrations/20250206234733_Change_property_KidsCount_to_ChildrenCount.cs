using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_property_KidsCount_to_ChildrenCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KidsCount",
                table: "Events",
                newName: "ChildrenCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChildrenCount",
                table: "Events",
                newName: "KidsCount");
        }
    }
}
