using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_I_M32COM.Data.Migrations
{
    public partial class Onetoone_event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Boat_typesId",
                table: "Events",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Boat_class_type",
                table: "Boat_Types",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_Boat_typesId",
                table: "Events",
                column: "Boat_typesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Boat_Types_Boat_typesId",
                table: "Events",
                column: "Boat_typesId",
                principalTable: "Boat_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Boat_Types_Boat_typesId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Boat_typesId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Boat_typesId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Boat_class_type",
                table: "Boat_Types",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
