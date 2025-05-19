using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Audio_And_Description_Fields_To_Working_Memory_Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WorkingMemoryResponses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WorkingMemoryResponses");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "WorkingMemoryResponses");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "WorkingMemoryResponses");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Admins");

            migrationBuilder.AddColumn<int>(
                name: "AudioId",
                table: "WorkingMemoryTests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkingMemoryTests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingMemoryTests_AudioId",
                table: "WorkingMemoryTests",
                column: "AudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingMemoryTests_Files_AudioId",
                table: "WorkingMemoryTests",
                column: "AudioId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingMemoryTests_Files_AudioId",
                table: "WorkingMemoryTests");

            migrationBuilder.DropIndex(
                name: "IX_WorkingMemoryTests_AudioId",
                table: "WorkingMemoryTests");

            migrationBuilder.DropColumn(
                name: "AudioId",
                table: "WorkingMemoryTests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkingMemoryTests");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WorkingMemoryResponses",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "WorkingMemoryResponses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "WorkingMemoryResponses",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "WorkingMemoryResponses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Admins",
                type: "integer",
                nullable: true);
        }
    }
}
