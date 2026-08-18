using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Postgres.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "departments",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                identifier = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                path = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                depth = table.Column<short>(type: "smallint", nullable: false),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_departments", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "locations",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                timezone = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_locations", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "positions",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_positions", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "department_locations",
            columns: table => new
            {
                department_id = table.Column<Guid>(type: "uuid", nullable: false),
                location_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_department_locations", x => new { x.department_id, x.location_id });
                table.ForeignKey(
                    name: "FK_department_locations_departments_department_id",
                    column: x => x.department_id,
                    principalTable: "departments",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_department_locations_locations_location_id",
                    column: x => x.location_id,
                    principalTable: "locations",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "department_positions",
            columns: table => new
            {
                department_id = table.Column<Guid>(type: "uuid", nullable: false),
                position_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_department_positions", x => new { x.department_id, x.position_id });
                table.ForeignKey(
                    name: "FK_department_positions_departments_department_id",
                    column: x => x.department_id,
                    principalTable: "departments",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_department_positions_positions_position_id",
                    column: x => x.position_id,
                    principalTable: "positions",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_department_locations_location_id",
            table: "department_locations",
            column: "location_id");

        migrationBuilder.CreateIndex(
            name: "IX_department_positions_position_id",
            table: "department_positions",
            column: "position_id");

        migrationBuilder.CreateIndex(
            name: "IX_departments_identifier",
            table: "departments",
            column: "identifier",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_departments_is_deleted",
            table: "departments",
            column: "is_deleted");

        migrationBuilder.CreateIndex(
            name: "IX_departments_parent_id",
            table: "departments",
            column: "parent_id");

        migrationBuilder.CreateIndex(
            name: "IX_departments_path",
            table: "departments",
            column: "path");

        migrationBuilder.CreateIndex(
            name: "IX_locations_is_deleted",
            table: "locations",
            column: "is_deleted");

        migrationBuilder.CreateIndex(
            name: "IX_positions_is_deleted",
            table: "positions",
            column: "is_deleted");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "department_locations");

        migrationBuilder.DropTable(
            name: "department_positions");

        migrationBuilder.DropTable(
            name: "locations");

        migrationBuilder.DropTable(
            name: "departments");

        migrationBuilder.DropTable(
            name: "positions");
    }
}
