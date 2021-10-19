using Microsoft.EntityFrameworkCore.Migrations;

namespace eTicket_Demo.Migrations
{
    public partial class AddUniqCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqCode",
                table: "Orders");
        }
    }
}
