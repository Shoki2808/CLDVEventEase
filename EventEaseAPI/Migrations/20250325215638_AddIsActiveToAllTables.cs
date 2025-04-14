using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            {
                migrationBuilder.AddColumn<bool>(
                    name: "IsActive",
                    table: "Admin",
                    type: "bit",
                    nullable: false,
                    defaultValue: true);

                migrationBuilder.AddColumn<bool>(
                    name: "IsActive",
                    table: "EventType",
                    type: "bit",
                    nullable: false,
                    defaultValue: true);

                migrationBuilder.AddColumn<bool>(
                    name: "IsActive",
                    table: "Venue",
                    type: "bit",
                    nullable: false,
                    defaultValue: true);

                migrationBuilder.AddColumn<bool>(
                    name: "IsActive",
                    table: "Event",
                    type: "bit",
                    nullable: false,
                    defaultValue: true);

                migrationBuilder.AddColumn<bool>(
                    name: "IsActive",
                    table: "EventVendor",
                    type: "bit",
                    nullable: false,
                    defaultValue: true);

                migrationBuilder.AddColumn<bool>(
                    name: "IsActive",
                    table: "Booking",
                    type: "bit",
                    nullable: false,
                    defaultValue: true);

                migrationBuilder.AddColumn<bool>(
                    name: "IsActive",
                    table: "Client",
                    type: "bit",
                    nullable: false,
                    defaultValue: true);

                migrationBuilder.AddColumn<bool>(
                    name: "IsActive",
                    table: "Payment",
                    type: "bit",
                    nullable: false,
                    defaultValue: true);



            }
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Client",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "EventVendor");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Venue");

            migrationBuilder.DropTable(
                name: "EventType");
        }
    }
}
