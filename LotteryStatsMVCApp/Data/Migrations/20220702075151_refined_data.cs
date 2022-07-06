using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryStatsMVCApp.Data.Migrations
{
    public partial class refined_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BallSet",
                table: "DrawHistory");

            migrationBuilder.DropColumn(
                name: "Machine",
                table: "DrawHistory");

            migrationBuilder.DropColumn(
                name: "UKMillionaireMaker",
                table: "DrawHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BallSet",
                table: "DrawHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Machine",
                table: "DrawHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UKMillionaireMaker",
                table: "DrawHistory",
                type: "text",
                nullable: true);
        }
    }
}
