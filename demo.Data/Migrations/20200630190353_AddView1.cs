using Microsoft.EntityFrameworkCore.Migrations;

namespace demo.Data.Migrations
{
    public partial class AddView1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW PlayerClubView
            AS SELECT p.Id as PlayerId,p.Name as PlayerName,c.Name as ClubName FROM Players as p
            INNER JOIN CLUBS AS c ON p.ClubId = c.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW PlayerClubView");
        }
    }
}
