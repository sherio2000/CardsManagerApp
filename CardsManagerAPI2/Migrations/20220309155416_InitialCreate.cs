using Microsoft.EntityFrameworkCore.Migrations;

namespace CardsManagerAPI2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardHolderName = table.Column<string>(type: "nVarchar(100)", nullable: true),
                    CustomerId = table.Column<string>(type: "nVarchar(100)", nullable: true),
                    CardNumber = table.Column<string>(type: "nVarchar(16)", nullable: true),
                    ExpirationDate = table.Column<string>(type: "nVarchar(5)", nullable: true),
                    SecurityCode = table.Column<string>(type: "nVarchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDetails");
        }
    }
}
