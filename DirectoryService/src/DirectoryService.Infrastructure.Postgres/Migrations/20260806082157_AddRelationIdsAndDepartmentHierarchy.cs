using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationIdsAndDepartmentHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_department_positions",
                table: "department_positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_department_locations",
                table: "department_locations");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "department_positions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "department_locations",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql(
                "UPDATE department_positions SET id = gen_random_uuid() WHERE id IS NULL;");

            migrationBuilder.Sql(
                "UPDATE department_locations SET id = gen_random_uuid() WHERE id IS NULL;");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "department_positions",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "department_locations",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_department_positions",
                table: "department_positions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_department_locations",
                table: "department_locations",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_department_positions_department_id_position_id",
                table: "department_positions",
                columns: new[] { "department_id", "position_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_locations_department_id_location_id",
                table: "department_locations",
                columns: new[] { "department_id", "location_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_departments_parent_id",
                table: "departments",
                column: "parent_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_departments_parent_id",
                table: "departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_department_positions",
                table: "department_positions");

            migrationBuilder.DropIndex(
                name: "IX_department_positions_department_id_position_id",
                table: "department_positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_department_locations",
                table: "department_locations");

            migrationBuilder.DropIndex(
                name: "IX_department_locations_department_id_location_id",
                table: "department_locations");

            migrationBuilder.DropColumn(
                name: "id",
                table: "department_positions");

            migrationBuilder.DropColumn(
                name: "id",
                table: "department_locations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_department_positions",
                table: "department_positions",
                columns: new[] { "department_id", "position_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_department_locations",
                table: "department_locations",
                columns: new[] { "department_id", "location_id" });
        }
    }
}
