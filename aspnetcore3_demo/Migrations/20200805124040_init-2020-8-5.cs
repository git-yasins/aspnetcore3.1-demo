using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnetcore3_demo.Migrations
{
    public partial class init202085 : Migration
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("19d42960-7635-4360-b25a-76f65793f352"), "Create Company", "Microsoft" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("fee4429e-4ed0-4b4b-9ad0-829e6d7dd496"), "SAMSUN Company", "SAMSUN" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("5fece8c6-afae-407a-96b6-c97aca57e4c2"), "SONY Company", "SONY" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("74f4655e-7206-4e85-98ff-695841ecd8b2"), "DELL Company", "DELL" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("947c05b7-b566-41b9-a466-e071f9c6c8e0"), "AMAZON Company", "AMAZON" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("7bfa2bc8-9748-4950-876e-9c733c4f5656"), "YOUTUBE Company", "YOUTUBE" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("4b56e067-c5de-4846-948d-0917f79501d7"), "FACEBOOK Company", "FACEBOOK" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("45f8183b-2258-4589-b37c-a4d7939bf4df"), "BILIBILI Company", "BILIBILI" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("939a8356-62ab-4701-8ca8-b34778c20a43"), "BAIDU Company", "BAIDU" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("eb0744ea-d315-4f7f-8a3c-14ae4cf5e3e7"), "ORACLE Company", "ORACLE" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("43a757aa-f01c-4f4c-ab6e-2f4a24647c9b"), "APPLIE Company", "APPLIE" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("201a3e4a-83bf-4e9c-ac82-8c45d6a57dfb"), "NOKIA Company", "NOKIA" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("2aa4335e-bfa1-4820-94f9-f7bb717a4997"), "HP Company", "HP" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("dc9377f0-ef11-42a2-89df-69279fd7f81d"), "Tongyong Company", "Tongyong" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("e85c5afe-389d-4eb1-9d8d-e8bd110a8092"), "Gaotong Company", "Gaotong" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("fb4d85e5-7b77-47f6-b223-e237706eb59a"), "HQ Company", "HQ" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("f72d336e-514d-41a1-a91e-f2eb0f4ddc46"), "WFT Company", "WFT" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("0f5c57b3-1635-4666-9bb8-64171c0fc6ec"), "Tencent Company", "Tencent" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("0d5c19d5-a289-402b-96a3-3135b6b03a52"), "Google Company", "Google" });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"), "Dont be evil", "Alibaba" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("b533a68e-8e64-46c2-afcf-b6b8b6c78982"), new Guid("2aa4335e-bfa1-4820-94f9-f7bb717a4997"), new DateTime(1985, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A001", "Mary", 2, "King" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7aa8d6ec-8088-457b-a547-d68cdabb96a6"), new Guid("2aa4335e-bfa1-4820-94f9-f7bb717a4997"), new DateTime(1995, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "A002", "Michl", 1, "Wang" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("4f4ea5a8-5d05-41f1-bbcb-67cf14f09472"), new Guid("7bfa2bc8-9748-4950-876e-9c733c4f5656"), new DateTime(1985, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "C001", "Aary", 2, "Fing" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("82491684-23c9-4ca7-b558-fe93ce663f59"), new Guid("0d5c19d5-a289-402b-96a3-3135b6b03a52"), new DateTime(1945, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "f001", "yary", 2, "ssng" });

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
                values: new object[] { new Guid("86fa9cf7-bfe2-46fa-b670-f3601641e689"), new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"), new DateTime(1995, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "C002", "Aichl", 1, "Fang" });

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
