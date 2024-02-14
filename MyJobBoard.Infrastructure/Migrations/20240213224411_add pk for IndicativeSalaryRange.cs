using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyJobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addpkforIndicativeSalaryRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IndicativeSalaryRange",
                table: "IndicativeSalaryRange");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "IndicativeSalaryRange",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndicativeSalaryRange",
                table: "IndicativeSalaryRange",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IndicativeSalaryRange_OpportunityId",
                table: "IndicativeSalaryRange",
                column: "OpportunityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IndicativeSalaryRange",
                table: "IndicativeSalaryRange");

            migrationBuilder.DropIndex(
                name: "IX_IndicativeSalaryRange_OpportunityId",
                table: "IndicativeSalaryRange");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IndicativeSalaryRange");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndicativeSalaryRange",
                table: "IndicativeSalaryRange",
                column: "OpportunityId");
        }
    }
}
