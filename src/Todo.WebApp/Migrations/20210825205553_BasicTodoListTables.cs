using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.WebApp.Migrations
{
    public partial class BasicTodoListTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "todo_list",
                columns: table => new
                {
                    todo_list_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_list", x => x.todo_list_id);
                });

            migrationBuilder.CreateTable(
                name: "todo_list_item",
                columns: table => new
                {
                    todo_list_item_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    todo_list_id = table.Column<int>(type: "INTEGER", nullable: false),
                    item_description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_list_item", x => x.todo_list_item_id);
                    table.ForeignKey(
                        name: "FK_todo_list_item_todo_list_todo_list_id",
                        column: x => x.todo_list_id,
                        principalTable: "todo_list",
                        principalColumn: "todo_list_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_todo_list_item_todo_list_id",
                table: "todo_list_item",
                column: "todo_list_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "todo_list_item");

            migrationBuilder.DropTable(
                name: "todo_list");
        }
    }
}
