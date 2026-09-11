using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAlmacenProductoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Productos");

            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad",
                table: "AlmacenProducto",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "AlmacenProducto");

            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad",
                table: "Productos",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
