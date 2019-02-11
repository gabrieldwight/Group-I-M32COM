using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_I_M32COM.Data.Migrations
{
    public partial class eventstablecreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event_Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Event_type_name = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Event_name = table.Column<string>(nullable: true),
                    Event_description = table.Column<string>(nullable: true),
                    Event_Start_date = table.Column<DateTime>(nullable: true),
                    Event_End_date = table.Column<DateTime>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Event_type_id = table.Column<int>(nullable: false),
                    Event_TypesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Event_Types_Event_TypesId",
                        column: x => x.Event_TypesId,
                        principalTable: "Event_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Event_TypesId",
                table: "Events",
                column: "Event_TypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Event_Types");
        }
    }
}
