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

        [HttpGet]
        [Route("list/{listId}")]
        public IActionResult Get(int listId)
        {
            var list = this.Db.TodoLists.Find(listId);
            var items = this.Db.TodoListItems.Where(i => i.TodoListId == listId);

            return this.Ok(new TodoList(
                listId,
                list.Title,
                items.Select(i => i.ItemDescription)
            ));
        }
    }
}
