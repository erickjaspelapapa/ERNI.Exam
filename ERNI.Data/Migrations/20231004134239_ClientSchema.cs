using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERNI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClientSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email_address = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    password = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    protected_pin = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client");
        }
    }
}
