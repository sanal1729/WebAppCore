using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Department = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Peoples",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 1, 1, "zia@zia.com", "Zia" });

            migrationBuilder.InsertData(
                table: "Peoples",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 2, 3, "tom@tom.com", "Tom" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peoples");
        }
    }
}
