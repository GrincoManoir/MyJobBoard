using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyJobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IndicativeSalaryRangenestedpropertyforopportunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndicativeSalaryRange");

            migrationBuilder.AddColumn<int>(
                name: "IndicativeSalaryRange_MaxSalary",
                table: "Opportunities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IndicativeSalaryRange_MinSalary",
                table: "Opportunities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IndicativeSalaryRange_Periodicity",
                table: "Opportunities",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndicativeSalaryRange_MaxSalary",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "IndicativeSalaryRange_MinSalary",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "IndicativeSalaryRange_Periodicity",
                table: "Opportunities");

            migrationBuilder.CreateTable(
                name: "IndicativeSalaryRange",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxSalary = table.Column<int>(type: "int", nullable: false),
                    MinSalary = table.Column<int>(type: "int", nullable: false),
                    OpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Periodicity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicativeSalaryRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndicativeSalaryRange_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndicativeSalaryRange_OpportunityId",
                table: "IndicativeSalaryRange",
                column: "OpportunityId",
                unique: true);
        }
    }
}
