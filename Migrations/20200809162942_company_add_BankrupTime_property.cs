using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnetcore3_demo.Migrations
{
    public partial class company_add_BankrupTime_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BankrupTime",
                table: "Companys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankrupTime",
                table: "Companys");
        }
    }
}
