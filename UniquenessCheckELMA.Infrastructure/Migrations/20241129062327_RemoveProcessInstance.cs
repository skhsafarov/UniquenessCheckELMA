using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniquenessCheckELMA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProcessInstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ProcessInstances_ProcessInstanceId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "ProcessInstances");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ProcessInstanceId",
                table: "Applications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstances", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ProcessInstanceId",
                table: "Applications",
                column: "ProcessInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ProcessInstances_ProcessInstanceId",
                table: "Applications",
                column: "ProcessInstanceId",
                principalTable: "ProcessInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
