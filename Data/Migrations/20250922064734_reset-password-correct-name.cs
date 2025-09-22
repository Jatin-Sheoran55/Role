using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class resetpasswordcorrectname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResetPasswordCode",
                table: "ResetPasswordCode");

            migrationBuilder.RenameTable(
                name: "ResetPasswordCode",
                newName: "ResetPasswordCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResetPasswordCodes",
                table: "ResetPasswordCodes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResetPasswordCodes",
                table: "ResetPasswordCodes");

            migrationBuilder.RenameTable(
                name: "ResetPasswordCodes",
                newName: "ResetPasswordCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResetPasswordCode",
                table: "ResetPasswordCode",
                column: "Id");
        }
    }
}
