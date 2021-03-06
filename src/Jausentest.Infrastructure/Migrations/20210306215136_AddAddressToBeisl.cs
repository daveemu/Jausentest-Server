using Microsoft.EntityFrameworkCore.Migrations;

namespace Jausentest.Infrastructure.Migrations
{
    public partial class AddAddressToBeisl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Beisl",
                newName: "Address_ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Beisl",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Beisl",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Beisl");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Beisl");

            migrationBuilder.RenameColumn(
                name: "Address_ZipCode",
                table: "Beisl",
                newName: "Address");
        }
    }
}
