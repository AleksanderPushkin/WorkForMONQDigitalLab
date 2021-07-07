using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkForMONQDigitalLab.WEBService.Migrations
{
    public partial class AddField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FailedMessage",
                table: "Mails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedMessage",
                table: "Mails");
        }
    }
}
