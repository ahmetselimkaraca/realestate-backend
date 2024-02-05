using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class withcoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Listings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Listings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Listings");
        }
    }
}
