using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_I_M32COM.Data.Migrations
{
    public partial class eventonetomanytest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Boat_TypesId",
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
                name: "IX_Events_Boat_TypesId",
                table: "Events",
                column: "Boat_TypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Boat_Types_Boat_TypesId",
                table: "Events",
                column: "Boat_TypesId",
                principalTable: "Boat_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Boat_Types_Boat_TypesId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Boat_TypesId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Boat_TypesId",
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
