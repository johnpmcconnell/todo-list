using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApp.DbQueries;
using Todo.WebApp.MvcModels;
using Todo.WebApp.SharedModels;
using static Todo.WebApp.Views.ViewDataNames;

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
            TodoList list;
            using (var trans = this.Db.Database.BeginTransaction())
            {
                list = this.Db.FetchTodoList(listId);
            }

            if (null == list)
            {
                return this.NotFoundView("This todo list does not exist");
            }

            return this.View("TodoList", list);
        }

        [HttpGet]
        [Route("list/" + ListIdPattern + "/edit")]
        public IActionResult Edit(int listId)
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var list = this.Db.FetchTodoList(listId);

                if (null == list)
                {
                    return this.NotFoundView($"Todo list with ID {listId} not found. Use {this.Url.Action(HtmlRouteActionNames.TodoListCreate)} to create a new list.");
                }

                this.ViewData[FormTitle] = "Edit";
                this.ViewData[SubmitOperationName] = "Save";
                return this.View("TodoListForm", list);
            }
        }

        [HttpPost]
        [HttpPut]
        [Route("list/" + ListIdPattern + "/edit")]
        public IActionResult SaveEdit(TodoListFullModel list)
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var oldList = this.Db.FetchTodoList(list.ListId);
                if (null == oldList)
                {
                    return this.NotFound($"Todo list with ID {list.ListId} not found. Use {this.Url.Action(HtmlRouteActionNames.TodoListCreate)} to create a new list.");
                }

                var newList = list.ToShared();
                this.Db.UpdateTodoList(newList);
                var response = this.RedirectRetrieveToAction(
                    HtmlRouteActionNames.TodoListGet,
                    new { listId = newList.Id }
                );

                trans.Commit();
                return response;
            }
        }

        [HttpGet]
        [Route("list/create")]
        public IActionResult Create()
        {
            this.ViewData[FormTitle] = "Create New";
            this.ViewData[SubmitOperationName] = "Create";
            return this.View("TodoListForm");
        }

        [HttpPost]
        [ActionName(HtmlRouteActionNames.TodoListCreate)]
        [Route("list/create")]
        public IActionResult Create(TodoListDataModel listData)
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var list = this.Db.CreateTodoList(listData.Title, listData.Items);
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
            TodoList list;
            using (var trans = this.Db.Database.BeginTransaction())
            {
                list = this.Db.FetchTodoList(listId);
            }

            if (null == list)
            {
                return this.NotFoundObject($"Todo list with ID {listId} not found");
            }

            return this.Ok(list);
        }

        [HttpPost]
        [HttpPut]
        [Route("api/list/" + ListIdPattern)]
        public IActionResult ApiEdit(TodoListFullModel list)
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var oldList = this.Db.FetchTodoList(list.ListId);
                if (null == oldList)
                {
                    return this.NotFoundObject($"Todo list with ID {list.ListId} not found. Use {this.Url.Action(ApiRouteActionNames.TodoListCreate)} to create a new list.");
                }

                var newList = list.ToShared();
                this.Db.UpdateTodoList(newList);
                trans.Commit();
                return this.Ok(newList);
            }
        }

        [HttpPost]
        [ActionName(ApiRouteActionNames.TodoListCreate)]
        [Route("api/list/create")]
        public IActionResult ApiCreate(TodoListDataModel listData)
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var list = this.Db.CreateTodoList(listData.Title, listData.Items);
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
