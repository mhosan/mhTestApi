using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mhTestApi.Migrations
{
    public partial class NuevaTablaClientMachineDos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientMachine",
                columns: table => new
                {
                    IdClientMachine = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MacAddress = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientMachine", x => x.IdClientMachine);
                });

            //migrationBuilder.CreateTable(
            //    name: "Tarea",
            //    columns: table => new
            //    {
            //        IdTarea = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        nombre = table.Column<string>(unicode: false, maxLength: 40, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Tarea__756A54024F95DB6D", x => x.IdTarea);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientMachine");

            migrationBuilder.DropTable(
                name: "Tarea");
        }
    }
}
