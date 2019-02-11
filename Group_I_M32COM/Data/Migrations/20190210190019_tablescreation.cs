using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_I_M32COM.Data.Migrations
{
    public partial class tablescreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boat_Crews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Boat_crew_name = table.Column<string>(nullable: true),
                    Boat_crew_address = table.Column<string>(nullable: true),
                    Boat_crew_phone = table.Column<string>(nullable: true),
                    Boat_crew_logo = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boat_Crews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boat_Media_Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Boat_media_type_name = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boat_Media_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boat_Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Boat_class_type = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boat_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sub_Boat_Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sub_boat_class_type = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Boat_TypesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sub_Boat_Types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sub_Boat_Types_Boat_Types_Boat_TypesId",
                        column: x => x.Boat_TypesId,
                        principalTable: "Boat_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Boat_name = table.Column<string>(nullable: true),
                    Boat_top_speed = table.Column<string>(nullable: true),
                    Boat_weight = table.Column<string>(nullable: true),
                    Boat_description = table.Column<string>(nullable: true),
                    Boat_media_type = table.Column<int>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Boat_TypesId = table.Column<int>(nullable: true),
                    Sub_Boat_TypesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boats_Boat_Types_Boat_TypesId",
                        column: x => x.Boat_TypesId,
                        principalTable: "Boat_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boats_Sub_Boat_Types_Sub_Boat_TypesId",
                        column: x => x.Sub_Boat_TypesId,
                        principalTable: "Sub_Boat_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boat_Medias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Boat_media_url = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    BoatId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boat_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boat_Medias_Boats_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boat_Medias_BoatId",
                table: "Boat_Medias",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_Boat_TypesId",
                table: "Boats",
                column: "Boat_TypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_Sub_Boat_TypesId",
                table: "Boats",
                column: "Sub_Boat_TypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Sub_Boat_Types_Boat_TypesId",
                table: "Sub_Boat_Types",
                column: "Boat_TypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boat_Crews");

            migrationBuilder.DropTable(
                name: "Boat_Media_Types");

            migrationBuilder.DropTable(
                name: "Boat_Medias");

            migrationBuilder.DropTable(
                name: "Boats");

            migrationBuilder.DropTable(
                name: "Sub_Boat_Types");

            migrationBuilder.DropTable(
                name: "Boat_Types");
        }
    }
}
