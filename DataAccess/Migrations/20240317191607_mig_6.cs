using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class mig_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversationID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PaymentToken",
                table: "Orders",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Orders",
                newName: "SendDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "Orders",
                newName: "PaymentToken");

            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "Orders",
                newName: "PaymentID");

            migrationBuilder.AddColumn<string>(
                name: "ConversationID",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
