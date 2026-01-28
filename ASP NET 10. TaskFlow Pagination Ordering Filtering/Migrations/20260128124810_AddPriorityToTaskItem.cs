using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityToTaskItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "TaskItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TaskItems");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
