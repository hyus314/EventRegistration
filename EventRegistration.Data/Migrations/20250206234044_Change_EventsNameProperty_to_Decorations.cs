using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_EventsNameProperty_to_Decorations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "Events",
                newName: "Decorations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decorations",
                table: "Events",
                newName: "EventName");
        }
    }
}
