using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace humanResourceManager.Migrations
{
    /// <inheritdoc />
    public partial class change_usersv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
