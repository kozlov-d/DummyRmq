using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DummyPublisher.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityInsteadOfMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DummyMessages");

            migrationBuilder.CreateTable(
                name: "DummyEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DummyEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DummyEntities");

            migrationBuilder.CreateTable(
                name: "DummyMessages",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    BatchIndex = table.Column<int>(type: "integer", nullable: false),
                    Fail = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DummyMessages", x => x.Guid);
                });
        }
    }
}
