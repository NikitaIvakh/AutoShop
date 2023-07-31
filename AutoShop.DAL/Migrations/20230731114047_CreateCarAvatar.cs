using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateCarAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Cars",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Cars");
        }
    }
}
