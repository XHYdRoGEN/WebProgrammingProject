using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProgrammingProject1.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryID",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserID",
                table: "Admins",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UsersUserID",
                table: "Admins",
                column: "UsersUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_UsersUserID",
                table: "Admins",
                column: "UsersUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryID",
                table: "Posts",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_UsersUserID",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UsersUserID",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UsersUserID",
                table: "Admins");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryID",
                table: "Posts",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
