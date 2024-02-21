using Microsoft.EntityFrameworkCore.Migrations;

namespace mhTestApi.Migrations
{
    public partial class addFieldIntAccessAcountAndFieldStringIpAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessCount",
                table: "ClientMachine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "ClientMachine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessCount",
                table: "ClientMachine");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "ClientMachine");
        }
    }
}
