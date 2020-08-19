using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnetcore3_demo.Migrations
{
    public partial class change_company_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Companys",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "Companys",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "Companys",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("0d5c19d5-a289-402b-96a3-3135b6b03a52"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "web", "search enging" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("0f5c57b3-1635-4666-9bb8-64171c0fc6ec"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "CN", "SoftWare", "electrophile,game" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "CN", "web", "electrophile" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("19d42960-7635-4360-b25a-76f65793f352"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "SoftWare", "SoftWare" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("201a3e4a-83bf-4e9c-ac82-8c45d6a57dfb"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "NL", "SoftWare", "phone" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("2aa4335e-bfa1-4820-94f9-f7bb717a4997"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "electrophile", "print" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("43a757aa-f01c-4f4c-ab6e-2f4a24647c9b"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "SoftWare", "electrophile,software" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("45f8183b-2258-4589-b37c-a4d7939bf4df"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "CN", "web", "web" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("4b56e067-c5de-4846-948d-0917f79501d7"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "web", "social contact" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("5fece8c6-afae-407a-96b6-c97aca57e4c2"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "JP", "hardware", "electrophile" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("74f4655e-7206-4e85-98ff-695841ecd8b2"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "hardware", "computer" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("7bfa2bc8-9748-4950-876e-9c733c4f5656"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "web", "social contact" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("939a8356-62ab-4701-8ca8-b34778c20a43"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "CN", "web", "search enging" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("947c05b7-b566-41b9-a466-e071f9c6c8e0"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "software", "web" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("dc9377f0-ef11-42a2-89df-69279fd7f81d"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "FR", "electrophile", "USA" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("e85c5afe-389d-4eb1-9d8d-e8bd110a8092"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "TW", "SoftWare", "USA" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("eb0744ea-d315-4f7f-8a3c-14ae4cf5e3e7"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "USA", "SoftWare", "db" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("f72d336e-514d-41a1-a91e-f2eb0f4ddc46"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "CN", "SoftWare", "web" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("fb4d85e5-7b77-47f6-b223-e237706eb59a"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "CN", "electrophile", "electrophile" });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: new Guid("fee4429e-4ed0-4b4b-9ad0-829e6d7dd496"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "KP", "hardware", "phone" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "Companys");
        }
    }
}
