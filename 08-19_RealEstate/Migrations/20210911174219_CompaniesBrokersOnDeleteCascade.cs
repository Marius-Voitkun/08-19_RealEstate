using Microsoft.EntityFrameworkCore.Migrations;

namespace _08_19_RealEstate.Migrations
{
    public partial class CompaniesBrokersOnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesBrokers_Brokers_BrokerId",
                table: "CompaniesBrokers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesBrokers_Companies_CompanyId",
                table: "CompaniesBrokers");

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesBrokers_Brokers_BrokerId",
                table: "CompaniesBrokers",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesBrokers_Companies_CompanyId",
                table: "CompaniesBrokers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesBrokers_Brokers_BrokerId",
                table: "CompaniesBrokers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesBrokers_Companies_CompanyId",
                table: "CompaniesBrokers");

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesBrokers_Brokers_BrokerId",
                table: "CompaniesBrokers",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesBrokers_Companies_CompanyId",
                table: "CompaniesBrokers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
