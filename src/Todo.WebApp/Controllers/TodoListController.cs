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
        [ActionName(HtmlRouteActionNames.TodoListGet)]
        [Route("list/{listId}")]
        public IActionResult Get(int listId)
        {
            var list = this.Db.FetchList(listId);

            return this.View("TodoList", list);
        }

        [HttpGet]
        [Route("list/create")]
        public IActionResult Create()
        {
            return this.View("TodoListCreate");
        }

        [HttpPost]
        [Route("list/create")]
        public IActionResult Create(string title, string[] items)
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var list = this.Db.CreateList(title, items);
                // Create result before commit in case of error
                var result = this.RedirectRetrieveToAction(
                    HtmlRouteActionNames.TodoListGet,
                    new { listId = list.Id }
                );
                trans.Commit();
                return result;
            }
        }

        [HttpGet]
        [ActionName(ApiRouteActionNames.TodoListGet)]
        [Route("api/list/{listId}")]
        public IActionResult ApiGet(int listId)
        {
            return this.Ok(this.Db.FetchList(listId));
        }

        [HttpPost]
        [Route("api/list/create")]
        public IActionResult ApiCreate(string title, string[] items)
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var list = this.Db.CreateList(title, items);
                // Create result before commit in case of error
                var result = this.Created(
                    this.Url.Action(
                        ApiRouteActionNames.TodoListGet,
                        new { listId = list.Id }
                    ),
                    list
                );
                trans.Commit();
                return result;
            }
        }
    }
}
