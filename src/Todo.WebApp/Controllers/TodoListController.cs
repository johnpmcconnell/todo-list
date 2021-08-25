using System;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApp.Models;

namespace Todo.WebApp.Controllers
{
    public class TodoListController : Controller
    {
        [HttpGet]
        [Route("list/{listId}")]
        public IActionResult Get(int listId)
        {
            return this.Ok(new TodoList(
                listId,
                "Shopping list",
                new[] { "Milk", "Eggs", "Bread" }
            ));
        }
    }
}
