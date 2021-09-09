using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApp.DataModels;
using Todo.WebApp.DbQueries;

namespace Todo.WebApp.Controllers
{
    public class TodoListController : Controller
    {
        private const string ListIdPattern = "{listId:int:min(1)}";

        private TodoListDbContext Db { get; }

        public TodoListController(TodoListDbContext db)
        {
            this.Db = db;
        }

        [HttpGet]
        [ActionName(HtmlRouteActionNames.TodoListGet)]
        [Route("list/" + ListIdPattern)]
        public IActionResult Get(int listId)
        {
            var list = this.Db.FetchList(listId);

            if (null == list)
            {
                return this.NotFoundView("This todo list does not exist");
            }

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
        [Route("api/list/" + ListIdPattern)]
        public IActionResult ApiGet(int listId)
        {
            var list = this.Db.FetchList(listId);

            if (null == list)
            {
                return this.NotFoundObject($"Todo list with ID {listId} not found");
            }

            return this.Ok(list);
        }

        [HttpPost]
        [Route("api/list/create")]
        public IActionResult ApiCreate(string title, string[] items)
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var list = this.Db.CreateList(title, items);
                // Create result before commit in case of error
                var result = this.CreatedAtAction(
                    ApiRouteActionNames.TodoListGet,
                    new { listId = list.Id },
                    list
                );
                trans.Commit();
                return result;
            }
        }
    }
}
