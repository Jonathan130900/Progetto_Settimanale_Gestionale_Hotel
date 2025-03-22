using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Progetto_Settimanale_Gestionale_Hotel.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "Nome");

            migrationBuilder.AddColumn<string>(
                name: "Cognome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cognome",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "AspNetUsers",
                newName: "FullName");
        }
    }
}
