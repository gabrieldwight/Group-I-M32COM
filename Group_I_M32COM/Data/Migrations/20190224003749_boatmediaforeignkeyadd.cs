using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_I_M32COM.Data.Migrations
{
    public partial class boatmediaforeignkeyadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boat_Medias_Boats_BoatId",
                table: "Boat_Medias");

            migrationBuilder.RenameColumn(
                name: "BoatId",
                table: "Boat_Medias",
                newName: "BoatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Boat_Medias_BoatId",
                table: "Boat_Medias",
                newName: "IX_Boat_Medias_BoatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boat_Medias_Boats_BoatsId",
                table: "Boat_Medias",
                column: "BoatsId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boat_Medias_Boats_BoatsId",
                table: "Boat_Medias");

            migrationBuilder.RenameColumn(
                name: "BoatsId",
                table: "Boat_Medias",
                newName: "BoatId");

            migrationBuilder.RenameIndex(
                name: "IX_Boat_Medias_BoatsId",
                table: "Boat_Medias",
                newName: "IX_Boat_Medias_BoatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boat_Medias_Boats_BoatId",
                table: "Boat_Medias",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
