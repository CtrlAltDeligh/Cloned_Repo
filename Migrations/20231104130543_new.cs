using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student.Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Gradings");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Gradings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Gradings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Gradings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
