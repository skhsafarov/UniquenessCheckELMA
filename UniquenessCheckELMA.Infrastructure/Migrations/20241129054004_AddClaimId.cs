using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniquenessCheckELMA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClaimId",
                table: "Applications",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "Applications");
        }
    }
}
