using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_I_M32COM.Data.Migrations
{
    public partial class boatcrewandeventparticipation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sub_boat_class_type",
                table: "Sub_Boat_Types",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Event_name",
                table: "Events",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Event_description",
                table: "Events",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Event_Start_date",
                table: "Events",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Event_End_date",
                table: "Events",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Boat_TypesId",
                table: "Events",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Event_type_name",
                table: "Event_Types",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Boat_class_type",
                table: "Boat_Types",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Boat_crew_name",
                table: "Boat_Crews",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Boat_crew_address",
                table: "Boat_Crews",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Boat_crew_allocation",
                table: "Boat_Crews",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Event_Participations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    position = table.Column<string>(nullable: true),
                    points_awarded = table.Column<int>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    EventId = table.Column<int>(nullable: true),
                    boat_CrewId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Participations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Participations_Boat_Crews_boat_CrewId",
                        column: x => x.boat_CrewId,
                        principalTable: "Boat_Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Member_name = table.Column<string>(maxLength: 50, nullable: false),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Boat_CrewId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Boat_Crews_Boat_CrewId",
                        column: x => x.Boat_CrewId,
                        principalTable: "Boat_Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Boat_TypesId",
                table: "Events",
                column: "Boat_TypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Participations_EventId",
                table: "Event_Participations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Participations_boat_CrewId",
                table: "Event_Participations",
                column: "boat_CrewId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Boat_CrewId",
                table: "Members",
                column: "Boat_CrewId");

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

            migrationBuilder.DropTable(
                name: "Event_Participations");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Events_Boat_TypesId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Boat_TypesId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Boat_crew_allocation",
                table: "Boat_Crews");

            migrationBuilder.AlterColumn<string>(
                name: "Sub_boat_class_type",
                table: "Sub_Boat_Types",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Event_name",
                table: "Events",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Event_description",
                table: "Events",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Event_Start_date",
                table: "Events",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Event_End_date",
                table: "Events",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Event_type_name",
                table: "Event_Types",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Boat_class_type",
                table: "Boat_Types",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Boat_crew_name",
                table: "Boat_Crews",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Boat_crew_address",
                table: "Boat_Crews",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
