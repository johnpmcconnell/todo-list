using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApp.DbQueries;

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
            var list = this.Db.FetchList(listId);

            return this.View("TodoList", list);
        }

        [HttpGet]
        [Route("api/list/{listId}")]
        public IActionResult ApiGet(int listId)
        {
            return this.Ok(this.Db.FetchList(listId));
        }
    }
}
