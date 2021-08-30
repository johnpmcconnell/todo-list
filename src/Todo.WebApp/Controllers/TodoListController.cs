using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApp.DataModels;

namespace Todo.WebApp.Controllers
{
    public class TodoListController : Controller
    {
        private TodoListDbContext Db { get; }

        public TodoListController(TodoListDbContext db)
        {
            this.Db = db;
        }

        public static TodoList FetchList(TodoListDbContext db, int listId)
        {
            var list = db.TodoLists.Find(listId);
            var items = db.TodoListItems.Where(i => i.TodoListId == listId);

            return new TodoList(
                listId,
                list.Title,
                items.Select(i => i.ItemDescription)
            );
        }

        [HttpGet]
        [Route("list/{listId}")]
        public IActionResult ApiGet(int listId)
        {
            return this.Ok(FetchList(this.Db, listId));
        }
    }
}
