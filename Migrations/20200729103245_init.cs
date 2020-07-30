using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnetcore3._1_demo.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Introduction = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    EmployeeNo = table.Column<string>(maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("19d42960-7635-4360-b25a-76f65793f352"), "Create Company", "Microsoft" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("d3da0df3-6097-40cc-9682-df4650bb34f5"), "aaa Company", "Google" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"), "Dont be evil", "Alibaba" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companys");
        }
    }
}
