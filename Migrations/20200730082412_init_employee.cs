using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnetcore3._1_demo.Migrations
{
    public partial class init_employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("62491684-23c9-4ca7-b558-fe93ce663fc9"), new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"), new DateTime(1945, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "B001", "Qary", 2, "Uing" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("bc801fd6-80e7-49d9-b239-0604fcc71b1e"), new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"), new DateTime(1937, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "B002", "Yichl", 1, "Ikng" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("4f4ea5a8-5d05-41f1-bbcb-67cf14f09472"), new Guid("d3da0df3-6097-40cc-9682-df4650bb34f5"), new DateTime(1985, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "C001", "Aary", 2, "Fing" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("86fa9cf7-bfe2-46fa-b670-f3601641e689"), new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"), new DateTime(1995, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "C002", "Aichl", 1, "Fang" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("b533a68e-8e64-46c2-afcf-b6b8b6c78982"), new Guid("19d42960-7635-4360-b25a-76f65793f352"), new DateTime(1985, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A001", "Mary", 2, "King" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7aa8d6ec-8088-457b-a547-d68cdabb96a6"), new Guid("19d42960-7635-4360-b25a-76f65793f352"), new DateTime(1995, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "A002", "Michl", 1, "Wang" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("4f4ea5a8-5d05-41f1-bbcb-67cf14f09472"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("62491684-23c9-4ca7-b558-fe93ce663fc9"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7aa8d6ec-8088-457b-a547-d68cdabb96a6"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("86fa9cf7-bfe2-46fa-b670-f3601641e689"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("b533a68e-8e64-46c2-afcf-b6b8b6c78982"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("bc801fd6-80e7-49d9-b239-0604fcc71b1e"));
        }
    }
}
