using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncurtadorLink.Database.Library.Migrations
{
    /// <inheritdoc />
    public partial class AdiçãodeindicenaFilaEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FilaEmail_Email",
                table: "FilaEmail",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilaEmail_Email",
                table: "FilaEmail");
        }
    }
}
