using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication16.Migrations
{
    public partial class AddedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { 1, "user1", "user1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { 2, "user2", "user2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { 3, "user3", "user3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
