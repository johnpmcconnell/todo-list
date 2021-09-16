using System;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApp.DbQueries;

namespace Todo.WebApp.Controllers
{
    public class HomeController : Controller
    {

        private TodoListDbContext Db { get; }

        public HomeController(TodoListDbContext db)
        {
            this.Db = db;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            using (var trans = this.Db.Database.BeginTransaction())
            {
                var summaries = this.Db.FetchTodoListSummaries();
                return this.View("Home", summaries);
            }
        }
    }
}
