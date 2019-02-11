using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_I_M32COM.Data.Migrations
{
    public partial class eventstablealtered1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Event_type_Id",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Event_type_Id",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }
    }
}
